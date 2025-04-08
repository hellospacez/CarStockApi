using CarStockApi.Data;
using CarStockApi.Endpoints.Car.Interfaces;
using CarStockApi.Models.Response.Car;
using FastEndpoints;

namespace CarStockApi.Endpoints;




public class GetCarsEndpoint : EndpointWithoutRequest<List<CarRecordModel>>
{
    private readonly ICarService _carService;

    public GetCarsEndpoint(ICarService carService)
    {
        _carService = carService;
    }

    public override void Configure()
    {
        Get("/cars");
        Claims("DealerId");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var dealerId = int.Parse(User.FindFirst("DealerId")!.Value);
        var cars = await _carService.GetCarsAsync(dealerId);
        await SendAsync(cars, cancellation: ct);
    }
}
