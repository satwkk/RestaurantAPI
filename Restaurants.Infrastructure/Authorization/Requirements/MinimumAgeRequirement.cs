using Microsoft.AspNetCore.Authorization;

namespace Restaurants.Infrastructure.Authorization.Requirements;

public class MinimumAgeRequirement : IAuthorizationRequirement
{
    public int Age { get; set; }
}