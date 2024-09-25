using MediatR;
using Restaurants.Application.Dishes.Dtos;

namespace Restaurants.Application.Dishes.Queries.GetDishFromRestaurantById;

public class GetDishFromRestaurantByIdQuery : IRequest<DishDto>
{
    public int RestaurantId { get; set; }
    public int FoodId { get; set; }

    public GetDishFromRestaurantByIdQuery(int restaurantId, int foodId)
    {
        RestaurantId = restaurantId;
        FoodId = foodId;
    }
}