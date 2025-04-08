using FluentValidation;

namespace CarStockApi.Models.Request.Car;

public class SearchCarsRequestModel
{
    public string? Make { get; set; }
    public string? Model { get; set; }
}


public class SearchCarsRequestValidator : AbstractValidator<SearchCarsRequestModel>
{
    public SearchCarsRequestValidator()
    {
        // Make validation (optional, max length 50)
        RuleFor(x => x.Make)
            .MaximumLength(50).WithMessage("Make cannot be longer than 50 characters.");

        // Model validation (optional, max length 50)
        RuleFor(x => x.Model)
            .MaximumLength(50).WithMessage("Model cannot be longer than 50 characters.");
    }
}