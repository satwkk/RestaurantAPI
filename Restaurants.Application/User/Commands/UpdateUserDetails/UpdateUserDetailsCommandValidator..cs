using FluentValidation;

namespace Restaurants.Application.Users.Commands;

public class UpdateUserDetailsCommandValidator : AbstractValidator<UpdateUserDetailsCommand>
{
    public UpdateUserDetailsCommandValidator()
    {
        RuleFor(c => c.DateOfBirth)
        .NotEmpty()
        .WithMessage("Provide a DOB");

        RuleFor(c => c.Nationality)
        .NotEmpty()
        .WithMessage("Provide a Nationality");
    }
}
