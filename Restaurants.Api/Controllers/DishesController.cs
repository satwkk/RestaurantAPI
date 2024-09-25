using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Application.Dishes.Queries.GetDishFromRestaurant;
using Restaurants.Application.Dishes.Commands.DeleteDishFromRestaurant;
using Restaurants.Application.Dishes.Queries.GetDishFromRestaurantById;

namespace Restaurants.Api.Controllers;

[Route("api/restaurant/{restaurantId}/dishes")]
[ApiController]
public class DishesController(IMediator mediator): ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateDish([FromRoute] int restaurantId, CreateDishCommand dishCommand)
    {
        dishCommand.RestaurantId = restaurantId;
        var dishId = await mediator.Send(dishCommand);
        return CreatedAtAction(nameof(GetDishForRestaurant), new { restaurantId, dishId });
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DishDto>>> GetAllForRestaurant([FromRoute] int restaurantId)
    {
        IEnumerable<DishDto> dishes = await mediator.Send(new GetDishFromRestaurantQuery(restaurantId));
        return Ok(dishes);
    }

    [HttpGet("{dishId}")]
    public async Task<ActionResult<DishDto>> GetDishForRestaurant([FromRoute] int restaurantId, [FromRoute] int dishId)
    {
        DishDto dish = await mediator.Send(new GetDishFromRestaurantByIdQuery(restaurantId, dishId));
        return Ok(dish);
    }

    [HttpDelete("{dishId}")]
    public async Task<IActionResult> DeleteDishFromRestaurant([FromRoute] int restaurantId, [FromRoute] int dishId)
    {
        await mediator.Send(new DeleteDishFromRestaurantCommand(restaurantId, dishId));
        return NoContent();
    }
}