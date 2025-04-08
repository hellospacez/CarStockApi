using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace CarStockApi.Models.Request.Car;

public class AddCarRequestModel {
    
    public string Make { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public int Stock { get; set; }
    
    
}

public class AddCarRequestValidator : AbstractValidator<AddCarRequestModel>
{
    public AddCarRequestValidator()
    {
        // Make validation
        RuleFor(x => x.Make)
            .NotEmpty().WithMessage("Make is required.")
            .Length(1, 50).WithMessage("Make cannot be longer than 50 characters.");

        // Model validation
        RuleFor(x => x.Model)
            .NotEmpty().WithMessage("Model is required.")
            .Length(1, 50).WithMessage("Model cannot be longer than 50 characters.");

        // Year validation
        RuleFor(x => x.Year)
            .InclusiveBetween(1900, 2100).WithMessage("Year must be between 1900 and 2100.");

        // Stock validation
        RuleFor(x => x.Stock)
            .InclusiveBetween(0, 9999).WithMessage("Stock must be a positive number.");
    }
}
