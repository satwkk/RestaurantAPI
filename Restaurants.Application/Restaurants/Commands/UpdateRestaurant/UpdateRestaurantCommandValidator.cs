using FluentValidation;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandValidator : AbstractValidator<UpdateRestaurantCommand>
{
    private List<bool> hasDeliveryPossibilities = [true, false];

    public UpdateRestaurantCommandValidator()
    {
        RuleFor(cmd => cmd.Name)
        .NotEmpty()
        .Length(3, 100)
        .WithMessage("Please provide a valid name for updating");

        RuleFor(cmd => cmd.Description)
        .NotEmpty()
        .WithMessage("Please provide a description field");

        RuleFor(cmd => cmd.HasDelivery)
        .Must(hasDeliveryPossibilities.Contains)
        .WithMessage("Please provide valid value (True or false)");
    }
}