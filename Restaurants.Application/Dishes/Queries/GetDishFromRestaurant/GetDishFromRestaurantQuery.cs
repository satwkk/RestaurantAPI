using MediatR;
using Restaurants.Application.Dishes.Dtos;

namespace Restaurants.Application.Dishes.Queries.GetDishFromRestaurant;

public class GetDishFromRestaurantQuery : IRequest<IEnumerable<DishDto>>
{
    public int RestaurantId { get; set; }

    public GetDishFromRestaurantQuery(int restaurantId)
    {
        RestaurantId = restaurantId;
    }
}