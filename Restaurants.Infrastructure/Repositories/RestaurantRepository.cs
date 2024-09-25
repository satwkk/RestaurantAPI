using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories;

internal class RestaurantRepository(RestaurantDbContext dbContext) : IRestaurantRepository
{
    public async Task<int> Create( Restaurant restaurantEntity )
    {
        dbContext.Restaurants.Add( restaurantEntity );
        await dbContext.SaveChangesAsync();
        return restaurantEntity.Id;
    }

    public async Task Delete(Restaurant entity)
    {
        dbContext.Remove(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Restaurant>> GetAllAsync()
    {
        var restaurants = await dbContext.Restaurants
            .Include(r => r.Dishes)
            .ToListAsync();
        return restaurants;
    }

    public async Task<Restaurant?> GetByIdAsync(int id)
    {
        var restaurant = await dbContext.Restaurants
            .Include(r => r.Dishes)
            .FirstOrDefaultAsync( x => x.Id == id );
        return restaurant;
    }

    public async Task Update(Restaurant entity)
    {
        var restaurant = await dbContext.Restaurants.FindAsync(entity.Id);
        if (restaurant is not null)
        {
            dbContext.Update(restaurant);
            await dbContext.SaveChangesAsync();
        }
    }
}
