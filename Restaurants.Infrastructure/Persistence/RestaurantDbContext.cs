using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;

namespace Restaurants.Infrastructure.Persistence;

internal class RestaurantDbContext( DbContextOptions<RestaurantDbContext> options ) : IdentityDbContext<User>(options)
{
    internal DbSet<Restaurant> Restaurants { get; set; }
    internal DbSet<Dish> Dishes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=RestaurantsDb;Trusted_Connection=True;");
    }

    protected override void OnModelCreating( ModelBuilder modelBuilder )
    {
        base.OnModelCreating( modelBuilder );

        // This configures that the address should be treated as a column inside Restaurant table 
        // and that the address doesn't need a GUID.
        modelBuilder.Entity<Restaurant>()
            .OwnsOne( r => r.Address );


        // This configures that the restaurant has many dishes ( one to many ) with HasMany()
        // And the dishes have one restaurant with WithOne()
        // And the dishes have a foreign key of RestaurantId
        modelBuilder.Entity<Restaurant>()
            .HasMany( r => r.Dishes )
            .WithOne()
            .HasForeignKey( d => d.RestaurantId );
    }
}