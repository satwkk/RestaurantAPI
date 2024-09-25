using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurant;
using Restaurants.Application.Restaurants.Queries.GetRestaurantById;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Microsoft.AspNetCore.Authorization;

namespace Restaurants.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/restaurants")]
public class RestaurantsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var restaurants = await mediator.Send(new GetAllRestaurantQuery());
        return Ok( restaurants );
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById( [FromRoute] int id )
    {
        var restaurant = await mediator.Send(new GetRestaurantByIdQuery(id));

        if (restaurant is null)
        {
            return NotFound();
        }
        return Ok( restaurant );
    }

    [HttpPost]
    public async Task<IActionResult> CreateRestaurant( CreateRestaurantCommand createRestaurantCommand)
    {
        var createdId = await mediator.Send(createRestaurantCommand);
        return CreatedAtAction( nameof( GetById ), new { id = createdId }, null );
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRestaurant([FromRoute] int id)
    {
        await mediator.Send(new DeleteRestaurantCommand(id));
        return NoContent();
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateRestaurant([FromRoute] int id, [FromBody] UpdateRestaurantCommand updateCommand) 
    {
        updateCommand.Id = id;
        await mediator.Send(updateCommand);
        return NoContent();
    }
}
