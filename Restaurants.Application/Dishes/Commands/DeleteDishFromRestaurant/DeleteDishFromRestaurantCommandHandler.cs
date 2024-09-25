using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.DeleteDishFromRestaurant;


public class DeleteDishFromRestaurantCommandHandler(ILogger<DeleteDishFromRestaurantCommandHandler> logger,
IRestaurantRepository restaurantRepository,
IDishesRepository dishesRepository): IRequestHandler<DeleteDishFromRestaurantCommand>
{
    public async Task Handle(DeleteDishFromRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting food {} from restaurant {}", request.FoodId, request.RestaurantId);
        var restaurant = await restaurantRepository.GetByIdAsync(request.RestaurantId);

        if (restaurant is null) 
        {
            throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
        }

        var dish = restaurant.Dishes.FirstOrDefault(d => d.Id == request.FoodId);

        if (dish is null)
        {
            throw new NotFoundException(nameof(Dish), request.FoodId.ToString());
        }

        await dishesRepository.Delete(dish);
    }
}