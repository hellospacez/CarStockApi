using CarStockApi.Models.Request.Car;
using CarStockApi.Models.Response.Car;

namespace CarStockApi.Endpoints.Car.Interfaces;

public interface ICarService
{
    Task AddCarAsync(int dealerId, AddCarRequest request);
    Task<List<CarRecord>> GetCarsAsync(int dealerId);
    Task<List<CarRecord>> SearchCarsAsync(int dealerId, string? make, string? model);
    Task<bool> UpdateCarStockAsync(int dealerId, int carId, int stock);
}