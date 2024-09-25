using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories;

internal class DishesRepository(RestaurantDbContext dbContext): IDishesRepository
{
    public async Task<int> Create(Dish dish)
    {
        var enteredDish = dbContext.Dishes.Add(dish);
        await dbContext.SaveChangesAsync();
        return enteredDish.Entity.Id;
    }

    public async Task Delete(Dish dish)
    {
        dbContext.Dishes.Remove(dish);
        await dbContext.SaveChangesAsync();
    }
}