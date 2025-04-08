using CarStockApi.Endpoints.Car.Interfaces;
using CarStockApi.Models.Request.Car;
using FastEndpoints;

namespace CarStockApi.Endpoints;

public class AddCarEndpoint : Endpoint<AddCarRequest>
{
    private readonly ICarService _carService;

    public AddCarEndpoint(ICarService carService)
    {
        _carService = carService;
    }

    public override void Configure()
    {
        Post("/cars");
        Claims("DealerId");
    }

    public override async Task HandleAsync(AddCarRequest req, CancellationToken ct)
    {
        var dealerId = int.Parse(User.FindFirst("DealerId")!.Value);
        await _carService.AddCarAsync(dealerId, req);
        await SendOkAsync(ct);
    }
}
