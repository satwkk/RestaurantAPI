using FluentValidation;

namespace Restaurants.Application.Dishes.Commands.CreateDish;

public class CreateDishCommandValidator : AbstractValidator<CreateDishCommand>
{
    public CreateDishCommandValidator()
    {
        RuleFor(d => d.Name)
        .NotEmpty()
        .Length(3, 100)
        .WithMessage("Please provide a valid name parameter");

        RuleFor(d => d.Description)
        .NotEmpty()
        .Length(3, 1000)
        .WithMessage("Please provide a valid description parameter");

        RuleFor(d => d.Price)
        .NotEmpty()
        .NotNull()
        .GreaterThanOrEqualTo(0)
        .WithMessage("Please provide a price parameter ( >= 0 )");

        RuleFor(d => d.KiloCalories)
        .GreaterThanOrEqualTo(0)
        .WithMessage("Please provide a kilo calories parameter (>= 0)");
    }
}