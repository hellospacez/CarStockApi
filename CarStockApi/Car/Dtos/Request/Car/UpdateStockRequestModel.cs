using FluentValidation;

namespace CarStockApi.Models.Request.Car;

public class UpdateStockRequestModel
{
    public int Stock { get; set; }
}

public class UpdateStockRequestValidator : AbstractValidator<UpdateStockRequestModel>
{
    public UpdateStockRequestValidator()
    {
        // Stock validation (ensure the stock is between 0 and 9999)
        RuleFor(x => x.Stock)
            .InclusiveBetween(0, 9999).WithMessage("Stock must be between 0 and 9999.");
    }
}