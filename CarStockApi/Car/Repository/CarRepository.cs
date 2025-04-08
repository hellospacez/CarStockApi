using CarStockApi.Data;
using CarStockApi.Endpoints.Car.Interfaces;
using CarStockApi.Models.Request.Car;
using CarStockApi.Models.Response.Car;
using Dapper;

namespace CarStockApi.Endpoints.Car.Repository;

  public class CarRepository : ICarRepository
    {
        private readonly DatabaseConnectionFactory _connectionFactory;

        public CarRepository(DatabaseConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task AddCarAsync(int dealerId, AddCarRequestModel requestModel)
        {
            using var conn = _connectionFactory.CreateConnection();
            await conn.ExecuteAsync(
                "INSERT INTO Cars (Make, Model, Year, Stock, DealerId) VALUES (@Make, @Model, @Year, @Stock, @DealerId)",
                new { requestModel.Make, requestModel.Model, requestModel.Year, requestModel.Stock, DealerId = dealerId });
        }

        public async Task<List<CarRecordModel>> GetCarsAsync(int dealerId)
        {
            using var conn = _connectionFactory.CreateConnection();
            var cars = await conn.QueryAsync<CarRecordModel>(
                "SELECT Id, Make, Model, Year, Stock FROM Cars WHERE DealerId = @DealerId", 
                new { DealerId = dealerId });
            return cars.ToList();
        }

        public async Task<List<CarRecordModel>> SearchCarsAsync(int dealerId, string? make, string? model)
        {
            using var conn = _connectionFactory.CreateConnection();
            var sql = """
                      SELECT Id, Make, Model, Year, Stock
                      FROM Cars
                      WHERE DealerId = @DealerId
                        AND (@Make IS NULL OR Make LIKE '%' || @Make || '%')
                        AND (@Model IS NULL OR Model LIKE '%' || @Model || '%')
                    """;
            var cars = await conn.QueryAsync<CarRecordModel>(sql, new { DealerId = dealerId, Make = make, Model = model });
            return cars.ToList();
        }

        public async Task<bool> UpdateCarStockAsync(int dealerId, int carId, int stock)
        {
            using var conn = _connectionFactory.CreateConnection();
            var exists = await conn.ExecuteScalarAsync<int>(
                "SELECT COUNT(*) FROM Cars WHERE Id = @Id AND DealerId = @DealerId",
                new { Id = carId, DealerId = dealerId });

            if (exists == 0)
                return false;

            await conn.ExecuteAsync("UPDATE Cars SET Stock = @Stock WHERE Id = @Id", new { Stock = stock, Id = carId });
            return true;
        }

        public async Task<bool> DeleteCarAsync(int dealerId, int carId)
        {
            using var conn = _connectionFactory.CreateConnection();
            var checkQuery = "SELECT COUNT(*) FROM Cars WHERE Id = @Id AND DealerId = @DealerId";
            var exists = await conn.ExecuteScalarAsync<int>(checkQuery, new { Id = carId, DealerId = dealerId });

            if (exists == 0)
                return false;

            var deleteQuery = "DELETE FROM Cars WHERE Id = @Id";
            await conn.ExecuteAsync(deleteQuery, new { Id = carId });
            return true;
        }
    }