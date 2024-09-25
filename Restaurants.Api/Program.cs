using Restaurants.Infrastructure.Extensions;
using Restaurants.Application.Extensions;
using Restaurants.Infrastructure.Seeders;
using Restaurants.Domain.Entities;
using Serilog;
using Restaurants.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication();
builder.Services.AddControllers();

builder.Services.AddScoped<ErrorHandlingMiddle>();

builder.Services.AddApplication();

builder.Services.AddInfrastructure(builder.Configuration);

// Add the serilog
builder.Host.UseSerilog((context, configuration) => 
{
    configuration.ReadFrom.Configuration(context.Configuration);
});

var app = builder.Build();

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();
await seeder.Seed();

// Exception middleware
app.UseMiddleware<ErrorHandlingMiddle>();

// add the serilog middleware to print api endpoints
app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

// Identity authentication
app.MapIdentityApi<User>();

app.UseAuthorization();

app.MapControllers();

app.Run();
