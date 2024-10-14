using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Users.Commands;
using Restaurants.Application.Users.Commands.AssignUserRole;
using Restaurants.Application.Users.Commands.RemoveUserRole;
using Restaurants.Domain.Constants;

namespace Restaurants.Api.Controllers;

[ApiController]
[Route("api/identity")]
public class IdentityController(IMediator mediator): ControllerBase
{
    [HttpPatch("user")]
    [Authorize]
    public async Task<IActionResult> UpdateUserDetails(UpdateUserDetailsCommand updateUserDetailsCommand) 
    {
        await mediator.Send(updateUserDetailsCommand);
        return NoContent();
    }

    [HttpPost("addUserRole")]
    [Authorize(Roles = UserRole.Admin)]
    public async Task<IActionResult> AssignUserRoles(AssignUserRoleCommand assignUserRoleCommand)
    {
        await mediator.Send(assignUserRoleCommand);
        return NoContent();
    }

    [HttpPost("removeUserRole")]
    [Authorize(Roles = UserRole.Admin)]
    public async Task<IActionResult> RemoveUserRoles(RemoveUserRoleCommand removeUserRoleCommand)
    {
        await mediator.Send(removeUserRoleCommand);
        return NoContent();
    }
}
