using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandHandler(ILogger<CreateRestaurantCommandHandler> logger, 
    IMapper mapper,
    IRestaurantRepository repository
    ) : IRequestHandler<CreateRestaurantCommand, int>
{
    public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation( "Creating a new restaurant" );
        var restaurantEntity = mapper.Map<Restaurant>( request );
        var id = await repository.Create( restaurantEntity );
        return id;
    }
}