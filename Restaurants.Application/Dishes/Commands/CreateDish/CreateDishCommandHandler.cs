using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.CreateDish;

public class CreateDishCommandHandler(ILogger<CreateDishCommandHandler> logger,
IMapper mapper,
IRestaurantRepository restaurantRepository,
IDishesRepository dishRepository) : IRequestHandler<CreateDishCommand, int>
{
    public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating a dish for restaurant with id: " + request.RestaurantId);
        var restaurant = restaurantRepository.GetByIdAsync(request.RestaurantId);
        if (restaurant is null) 
        {
            throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
        }
        var dishEntity = mapper.Map<Dish>(request);
        var id = await dishRepository.Create(dishEntity);
        logger.LogInformation("Created dish with id: " + id);
        return id;
    }
}