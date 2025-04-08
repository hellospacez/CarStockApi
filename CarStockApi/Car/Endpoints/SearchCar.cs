using CarStockApi.Data;
using CarStockApi.Endpoints.Car.Interfaces;
using CarStockApi.Models.Request.Car;
using CarStockApi.Models.Response.Car;
using FastEndpoints;

namespace CarStockApi.Endpoints;





public class SearchCarsEndpoint : Endpoint<SearchCarsRequestModel, List<CarRecordModel>>
{
    private readonly ICarService _carService;

    public SearchCarsEndpoint(ICarService carService)
    {
        _carService = carService;
    }

    public override void Configure()
    {
        Get("/cars/search");
        Claims("DealerId"); 
    }

    public override async Task HandleAsync(SearchCarsRequestModel req, CancellationToken ct)
    {
        var dealerId = int.Parse(User.FindFirst("DealerId")!.Value);

        var result = await _carService.SearchCarsAsync(dealerId, req.Make, req.Model);

        await SendAsync(result, cancellation: ct);
    }
}

