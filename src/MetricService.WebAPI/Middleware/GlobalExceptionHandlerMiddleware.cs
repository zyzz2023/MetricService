using Microsoft.AspNetCore.Mvc;

namespace MetricService.WebAPI.Middleware;

public class GlobalExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

    public GlobalExceptionHandlerMiddleware(
        RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch(Exception ex) 
        {
            _logger.LogError($"Exception: {ex.Source}, {ex.Message}, {ex.InnerException?.Message}");

            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var problemDetails = new ProblemDetails
        {
            Instance = context.Request.Path,
            Type = ex.GetType().Name,
            Status = StatusCodes.Status500InternalServerError,
            Title = ex.Message,
            Detail = $"{ex.Message} | {ex.InnerException?.Message}"
        };

        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        await context.Response.WriteAsJsonAsync(problemDetails);
    }
}
