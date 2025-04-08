using CarStockApi.Endpoints.Car.Interfaces;
using CarStockApi.Models.Request.Car;
using FastEndpoints;


namespace CarStockApi.Endpoints;


public class UpdateCarStockEndpoint : Endpoint<UpdateStockRequest>
{
    private readonly ICarService _carService;

    public UpdateCarStockEndpoint(ICarService carService)
    {
        _carService = carService;
    }

    public override void Configure()
    {
        Put("/cars/{id}/stock");
        Claims("DealerId");
    }

    public override async Task HandleAsync(UpdateStockRequest req, CancellationToken ct)
    {
        var dealerId = int.Parse(User.FindFirst("DealerId")!.Value);
        var carId = Route<int>("id");

        var success = await _carService.UpdateCarStockAsync(dealerId, carId, req.Stock);

        if (!success)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendOkAsync(ct);
    }
}
