namespace Restaurants.Application.Users;

public record CurrentUser(string Id, string Email, IEnumerable<string> Roles, string? DateOfBirthString, DateOnly? DateOfBirth)
{
    public bool IsInRole(string role)
    {
        return Roles.Contains(role);
    }
}