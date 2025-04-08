using CarStockApi.Endpoints.Car.Interfaces;
using CarStockApi.Models.Request.Car;
using CarStockApi.Models.Response.Car;
using FastEndpoints;
using FluentValidation.Results;

namespace CarStockApi.Endpoints;

#region CarCreate

public class CarCreate : Endpoint<AddCarRequestModel>
{
    private readonly ICarService _carService;

    public CarCreate(ICarService carService)
    {
        _carService = carService;
    }

    public override void Configure()
    {
        Post("/car");
        Validator<AddCarRequestValidator>();
        Claims("DealerId");
    }

    public override async Task HandleAsync(AddCarRequestModel req, CancellationToken ct)
    {
 
        var dealerId = int.Parse(User.FindFirst("DealerId")!.Value);
        await _carService.AddCarAsync(dealerId, req);
        await SendOkAsync(ct);
    }
}

#endregion

#region CarDelete

public class CarDelete : Endpoint<DeleteCarRequestModel>
{
    private readonly ICarService _carService;

    public CarDelete(ICarService carService)
    {
        _carService = carService;
    }

    public override void Configure()
    {
        Delete("/car/{id}");
        Validator<DeleteCarRequestValidator>();
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

#endregion

#region CarsGet
public class CarsGet : Endpoint<SearchCarsRequestModel, List<CarRecordModel>>
{
    private readonly ICarService _carService;
    public CarsGet(ICarService carService) => _carService = carService;

    public override void Configure()
    {
        Get("/car");
        Validator<SearchCarsRequestValidator>();
        Claims("DealerId");
        Summary(s =>
        {
            s.Summary = "Get all or filtered cars";
            s.Description = "Returns all cars if no filter is provided; otherwise applies filtering.";
        });
    }

    public override async Task HandleAsync(SearchCarsRequestModel req, CancellationToken ct)
    {
        var dealerId = int.Parse(User.FindFirst("DealerId")!.Value);
        var cars = await _carService.GetCarsAsync(dealerId, req);
        await SendAsync(cars, cancellation: ct);
    }
}
#endregion

#region CarPut

public class CarPut : Endpoint<UpdateStockRequestModel>
{
    private readonly ICarService _carService;

    public CarPut(ICarService carService)
    {
        _carService = carService;
    }

    public override void Configure()
    {
        Put("/car/{id}");
        Validator<UpdateStockRequestValidator>();
        Claims("DealerId");
    }

    public override async Task HandleAsync(UpdateStockRequestModel req, CancellationToken ct)
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

#endregion