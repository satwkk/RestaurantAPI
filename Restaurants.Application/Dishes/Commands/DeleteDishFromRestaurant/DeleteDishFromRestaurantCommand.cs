using MediatR;

namespace Restaurants.Application.Dishes.Commands.DeleteDishFromRestaurant;

public class DeleteDishFromRestaurantCommand : IRequest
{
    public int RestaurantId { get; set; }
    public int FoodId { get; set; }

    public DeleteDishFromRestaurantCommand(int restaurantId, int foodId)
    {
        RestaurantId = restaurantId;
        FoodId = foodId;
    }
}