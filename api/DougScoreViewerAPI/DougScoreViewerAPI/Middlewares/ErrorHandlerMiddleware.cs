using System.Net;
using System.Text.Json;
using DougScoreViewerAPI.Models;

namespace DougScoreViewerAPI.Middlewares;

public class ErrorHandlerMiddleware 
{
    private readonly RequestDelegate _next;
    private readonly IWebHostEnvironment _env;

    public ErrorHandlerMiddleware(IWebHostEnvironment env, RequestDelegate next)
    {
        _env = env;
        _next = next;
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

            string result;
            if (_env.EnvironmentName == "Development")
            {
                result = JsonSerializer.Serialize(new DeveloperErrorResponse(
                    error.Message,
                    error.StackTrace,
                    error.Source,
                    error.InnerException?.Message));
            }
            else
            {
                var message = response.StatusCode == (int)HttpStatusCode.InternalServerError ? "Oops! Something went wrong." : error?.Message;
                result = JsonSerializer.Serialize(new { message = message });
            }

            await response.WriteAsync(result);
        }
    }
}

public record DeveloperErrorResponse(string? Message, string? StackTrace, string? Source, string? InnerExceptionMessage);