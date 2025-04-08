using CarStockApi.Endpoints.Car.Interfaces;
using CarStockApi.Models.Request.Car;
using FastEndpoints;

namespace CarStockApi.Endpoints;

public class DeleteCarEndpoint : Endpoint<DeleteCarRequestModel>
{
    private readonly ICarService _carService;

    public DeleteCarEndpoint(ICarService carService)
    {
        _carService = carService;
    }

    public override void Configure()
    {
        Delete("/cars/{id}"); 
        Claims("DealerId"); 
    }

    public override async Task HandleAsync(DeleteCarRequestModel req, CancellationToken ct)
    {
        var dealerId = int.Parse(User.FindFirst("DealerId")!.Value); 
        var result = await _carService.DeleteCarAsync(dealerId, req.Id);

        if (result)
        {
            await SendOkAsync(ct); 
        }
        else
        {
            await SendNotFoundAsync(ct); 
        }
    }
}