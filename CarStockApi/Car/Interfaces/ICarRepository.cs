using CarStockApi.Models.Request.Car;
using CarStockApi.Models.Response.Car;

namespace CarStockApi.Endpoints.Car.Interfaces;

public interface ICarRepository
{
    Task AddCarAsync(int dealerId, AddCarRequestModel requestModel);
    Task<List<CarRecordModel>> GetCarsAsync(int dealerId, SearchCarsRequestModel filter);
    Task<bool> UpdateCarStockAsync(int dealerId, int carId, int stock);
    Task<bool> DeleteCarAsync(int dealerId, int carId);
}