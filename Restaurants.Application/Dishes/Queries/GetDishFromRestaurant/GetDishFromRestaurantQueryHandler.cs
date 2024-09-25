using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Queries.GetDishFromRestaurant;

public class GetDishFromRestaurantQueryHandler(ILogger<GetDishFromRestaurantQueryHandler> logger,
IRestaurantRepository restaurantRepository,
IMapper mapper) : IRequestHandler<GetDishFromRestaurantQuery, IEnumerable<DishDto>>
{
    public async Task<IEnumerable<DishDto>> Handle(GetDishFromRestaurantQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting dishes for restaurant: " + request.RestaurantId);
        var restaurant = await restaurantRepository.GetByIdAsync(request.RestaurantId);
        if (restaurant is null) 
        {
            throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
        }
        return mapper.Map<IEnumerable<DishDto>>(restaurant.Dishes);
    }
}
