using System.Net;

namespace HappyInventory.API.Middlewares;

public class ExeptionsMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IHostEnvironment _hostEnvironment;
    private readonly ILogger<ExeptionsMiddleware> _logger;
    public ExeptionsMiddleware(RequestDelegate next, IHostEnvironment hostEnvironment, ILogger<ExeptionsMiddleware> logger)
    {
        _next = next;
        _hostEnvironment = hostEnvironment;
        _logger = logger;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception has occurred");

                if (!context.Response.HasStarted)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var response = new
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = _hostEnvironment.IsDevelopment()
                            ? ex.Message
                            : "An internal server error occurred",
                        Details = _hostEnvironment.IsDevelopment()
                            ? ex.StackTrace
                            : null
                    };

                    await context.Response.WriteAsJsonAsync(response);
                }
            }
        }
    }

}

public static class ExceptionsMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionsMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExeptionsMiddleware>();
    }
}