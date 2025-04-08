using CarStockApi.Data;
using CarStockApi.Endpoints.Car.Interfaces;
using CarStockApi.Models.Request.Car;
using CarStockApi.Models.Response.Car;
using Dapper;
using Microsoft.Data.Sqlite;

namespace CarStockApi.Endpoints.Car.Services;

public class CarService : ICarService
{
    public async Task AddCarAsync(int dealerId, AddCarRequest request)
    {
        using var conn = new SqliteConnection(Database.ConnectionString);

        await conn.ExecuteAsync(
            "INSERT INTO Cars (Make, Model, Year, Stock, DealerId) VALUES (@Make, @Model, @Year, @Stock, @DealerId)",
            new
            {
                request.Make,
                request.Model,
                request.Year,
                request.Stock,
                DealerId = dealerId
            });
    }
    
    
    public async Task<List<CarRecord>> GetCarsAsync(int dealerId)
    {
        using var conn = new SqliteConnection(Database.ConnectionString);

        var cars = await conn.QueryAsync<CarRecord>(
            "SELECT Id, Make, Model, Year, Stock FROM Cars WHERE DealerId = @DealerId",
            new { DealerId = dealerId });

        return cars.ToList();
    }
    
    public async Task<List<CarRecord>> SearchCarsAsync(int dealerId, string? make, string? model)
    {
        using var conn = new SqliteConnection(Database.ConnectionString);

        var sql = """
                  SELECT Id, Make, Model, Year, Stock
                  FROM Cars
                  WHERE DealerId = @DealerId
                    AND (@Make IS NULL OR Make LIKE '%' || @Make || '%')
                    AND (@Model IS NULL OR Model LIKE '%' || @Model || '%')
                  """;

        var cars = await conn.QueryAsync<CarRecord>(sql, new
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

    
}
