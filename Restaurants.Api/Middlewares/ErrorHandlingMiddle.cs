
using Restaurants.Domain.Exceptions;

namespace Restaurants.Api.Middlewares;

public class ErrorHandlingMiddle(ILogger<ErrorHandlingMiddle> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try 
        {
            await next.Invoke(context);
        }
        catch (NotFoundException NotFound)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync(NotFound.Message);
            logger.LogWarning(NotFound.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("Something went wrong");
        }
    }
}