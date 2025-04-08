using FluentValidation;

namespace CarStockApi.Models.Request.Car;

public class DeleteCarRequestModel
{
    public int Id { get; set; } 
}


public class DeleteCarRequestValidator : AbstractValidator<DeleteCarRequestModel>
{
    public DeleteCarRequestValidator()
    {
        // Id validation
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Car ID must be a positive integer.");
    }
}