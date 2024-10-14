using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;

namespace Restaurants.Infrastructure.Authorization.Requirements;

public class MinimumAgeRequirementHandler(ILogger<MinimumAgeRequirementHandler> logger,
IUserContext userContext) : AuthorizationHandler<MinimumAgeRequirement>
{
    protected async override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
    {
        var user = userContext.GetCurrentUser();

        logger.LogInformation("User: {Email}, date of birth: {DoB} - Handling minimum requirements", 
        user.Email, user.DateOfBirth);

        if (user.DateOfBirth.Value.AddYears(requirement.Age) <= DateOnly.FromDateTime(DateTime.Now))
        {
            logger.LogInformation("Authorization succeeded");
            context.Succeed(requirement);
        }
    }
}