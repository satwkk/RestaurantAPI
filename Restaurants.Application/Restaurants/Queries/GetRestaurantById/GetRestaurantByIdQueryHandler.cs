using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.Restaurants.Queries.GetRestaurantById;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurant;

public class GetRestaurantByIdQueryHandler(ILogger<GetRestaurantByIdQueryHandler> logger,
IMapper mapper,
IRestaurantRepository repository) : IRequestHandler<GetRestaurantByIdQuery, RestaurantDto>
{
    public async Task<RestaurantDto> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation( "Getting a restaurant by id: " + request.Id );
        var restaurant = await repository.GetByIdAsync( request.Id )
        ??  throw new NotFoundException(nameof(Restaurant), request.Id.ToString());
        var restaurantDto = mapper.Map<RestaurantDto>( restaurant );
        return restaurantDto;
    }
}