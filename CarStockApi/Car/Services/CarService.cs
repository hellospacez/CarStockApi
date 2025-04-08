using CarStockApi.Endpoints.Car.Interfaces;
using CarStockApi.Models.Request.Car;
using CarStockApi.Models.Response.Car;


namespace CarStockApi.Endpoints.Car.Services;

public class CarService : ICarService
{
    private readonly ICarRepository _carRepository;

    public CarService(ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }

    public async Task AddCarAsync(int dealerId, AddCarRequestModel requestModel)
    {
        await _carRepository.AddCarAsync(dealerId, requestModel);
    }

    public async Task<List<CarRecordModel>> GetCarsAsync(int dealerId, SearchCarsRequestModel filter)
    {
        return await _carRepository.GetCarsAsync(dealerId, filter);
    }

    public async Task<bool> UpdateCarStockAsync(int dealerId, int carId, int stock)
    {
        return await _carRepository.UpdateCarStockAsync(dealerId, carId, stock);
    }

    public async Task<bool> DeleteCarAsync(int dealerId, int carId)
    {
        return await _carRepository.DeleteCarAsync(dealerId, carId);
    }
}