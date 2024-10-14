using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Authorization;
using Restaurants.Infrastructure.Persistence;
using Restaurants.Infrastructure.Repositories;
using Restaurants.Infrastructure.Seeders;

namespace Restaurants.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString( "RestaurantsDb" );
        
        services.AddDbContext<RestaurantDbContext>( options =>
            options.UseSqlServer( connectionString ).EnableSensitiveDataLogging() 
        );

        // Registering the identity service for authentication
        services.AddIdentityApiEndpoints<User>()
        .AddRoles<IdentityRole>()
        .AddClaimsPrincipalFactory<RestaurantUserClaimsPrincipalFactory>()
        .AddEntityFrameworkStores<RestaurantDbContext>();

        services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
        services.AddScoped<IRestaurantRepository, RestaurantRepository>();
        services.AddScoped<IDishesRepository, DishesRepository>();
        services.AddAuthorizationBuilder()
        .AddPolicy("HasNationality", builder => builder.RequireClaim("Nationality"))
        .AddPolicy("IsAge18", pol => pol.AddRequirements());
        // .AddPolicy("HasNationality", builder => builder.RequireClaim("Nationality", "German", "Indian"));
    }
}
