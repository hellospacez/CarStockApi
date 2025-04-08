using CarStockApi.Models.Request.Car;
using CarStockApi.Models.Response.Car;

namespace CarStockApi.Endpoints.Car.Interfaces;

public interface ICarService
{
    Task AddCarAsync(int dealerId, AddCarRequestModel requestModel);
    Task<List<CarRecordModel>> GetCarsAsync(int dealerId);
    Task<List<CarRecordModel>> SearchCarsAsync(int dealerId, string? make, string? model);
    Task<bool> UpdateCarStockAsync(int dealerId, int carId, int stock);
    Task<bool> DeleteCarAsync(int dealerId, int carId);
}