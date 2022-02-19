using System.Net;
using System.Text.Json;
using DougScoreViewerAPI.Models;

namespace DougScoreViewerAPI.Middlewares;

public class ErrorHandlerMiddleware 
{
    private readonly RequestDelegate _next;
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<ErrorHandlerMiddleware> _logger;

    public ErrorHandlerMiddleware(IWebHostEnvironment env, RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
    {
        _env = env;
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            switch (error)
            {
                case AppException e:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case KeyNotFoundException e:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            if (_env.EnvironmentName == "Development")
            {
                _logger.LogError(error, "Error!");
            }

            var message = response.StatusCode == (int)HttpStatusCode.InternalServerError ? "Oops! Something went wrong." : error?.Message;
            var result = JsonSerializer.Serialize(new {  message, successful = false });
            
            await response.WriteAsync(result);
        }
    }
}

public record DeveloperErrorResponse(string? Message, string? StackTrace, string? Source, string? InnerExceptionMessage);