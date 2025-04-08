using CarStockApi.Data;
using CarStockApi.Endpoints.Car.Interfaces;
using CarStockApi.Models.Request.Car;
using CarStockApi.Models.Response.Car;
using Dapper;
using Microsoft.Data.Sqlite;

namespace CarStockApi.Endpoints.Car.Services;

public class CarService : ICarService
{
    public async Task AddCarAsync(int dealerId, AddCarRequestModel requestModel)
    {
        using var conn = new SqliteConnection(Database.ConnectionString);

        await conn.ExecuteAsync(
            "INSERT INTO Cars (Make, Model, Year, Stock, DealerId) VALUES (@Make, @Model, @Year, @Stock, @DealerId)",
            new
            {
                requestModel.Make,
                requestModel.Model,
                requestModel.Year,
                requestModel.Stock,
                DealerId = dealerId
            });
    }
    
    
    public async Task<List<CarRecordModel>> GetCarsAsync(int dealerId)
    {
        using var conn = new SqliteConnection(Database.ConnectionString);

        var cars = await conn.QueryAsync<CarRecordModel>(
            "SELECT Id, Make, Model, Year, Stock FROM Cars WHERE DealerId = @DealerId",
            new { DealerId = dealerId });

        return cars.ToList();
    }
    
    public async Task<List<CarRecordModel>> SearchCarsAsync(int dealerId, string? make, string? model)
    {
        using var conn = new SqliteConnection(Database.ConnectionString);

        var sql = """
                  SELECT Id, Make, Model, Year, Stock
                  FROM Cars
                  WHERE DealerId = @DealerId
                    AND (@Make IS NULL OR Make LIKE '%' || @Make || '%')
                    AND (@Model IS NULL OR Model LIKE '%' || @Model || '%')
                  """;

        var cars = await conn.QueryAsync<CarRecordModel>(sql, new
        {
            DealerId = dealerId,
            Make = make,
            Model = model
        });

        return cars.ToList();
    }
    
    
    public async Task<bool> UpdateCarStockAsync(int dealerId, int carId, int stock)
    {
        using var conn = new SqliteConnection(Database.ConnectionString);

        var exists = await conn.ExecuteScalarAsync<int>(
            "SELECT COUNT(*) FROM Cars WHERE Id = @Id AND DealerId = @DealerId",
            new { Id = carId, DealerId = dealerId });

        if (exists == 0)
            return false;

        await conn.ExecuteAsync(
            "UPDATE Cars SET Stock = @Stock WHERE Id = @Id",
            new { Stock = stock, Id = carId });

        return true;
    }

    public async Task<bool> DeleteCarAsync(int dealerId, int carId)
    {
        using var conn = new SqliteConnection(Database.ConnectionString);
        var checkQuery = "SELECT COUNT(*) FROM Cars WHERE Id = @Id AND DealerId = @DealerId";
        var exists = await conn.ExecuteScalarAsync<int>(checkQuery, new { Id = carId, DealerId = dealerId });

        if (exists == 0)
            return false;

        var deleteQuery = "DELETE FROM Cars WHERE Id = @Id";
        await conn.ExecuteAsync(deleteQuery, new { Id = carId });
        return true;
    }

}
