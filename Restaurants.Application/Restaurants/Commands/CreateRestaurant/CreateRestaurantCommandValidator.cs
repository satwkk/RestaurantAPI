using FluentValidation;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants.Validators;

public class CreateRestaurantCommandValidator : AbstractValidator<CreateRestaurantCommand>
{
    private readonly List<string> validCategories = ["Italian", "Mexican", "Japanese", "American", "Indian"];
    public CreateRestaurantCommandValidator()
    {
        RuleFor( cmd => cmd.Name )
            .Length( 3, 100 );

        RuleFor( cmd => cmd.Description )
            .NotEmpty()
            .WithMessage( "Description is required !" );

        RuleFor( cmd => cmd.Category )
            .Must( validCategories.Contains )
            .WithMessage( "Invalid category. Please choose from the valid categories" );

        RuleFor( cmd => cmd.ContactEmail )
            .EmailAddress()
            .WithMessage( "Please provide valid email address." );

        RuleFor( cmd => cmd.PostalCode )
            .Matches( @"^\d{2}-\d{3}$" )
            .WithMessage( "Please provide a valid postal code (XX-XXX)" );
    }
}
