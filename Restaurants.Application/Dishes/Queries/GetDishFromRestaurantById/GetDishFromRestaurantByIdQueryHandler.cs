using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Queries.GetDishFromRestaurantById;

public class GetDishFromRestaurantByIdQueryHandler(ILogger<GetDishFromRestaurantByIdQueryHandler> logger,
IMapper mapper,
IRestaurantRepository restaurantRepository) : IRequestHandler<GetDishFromRestaurantByIdQuery, DishDto>
{
    public async Task<DishDto> Handle(GetDishFromRestaurantByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Getting dish with id {request.FoodId} from restaurant {request.RestaurantId}");

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

        return mapper.Map<DishDto>(dish);
    }
}
