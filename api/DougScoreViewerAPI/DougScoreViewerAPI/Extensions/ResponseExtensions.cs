using DougScoreViewerAPI.Models;

namespace DougScoreViewerAPI.Extensions;

public static class ResponseExtensions
{
    public static ApiResponse<T> ToApiResponse<T>(this ServiceResponse<T> serviceResponse, string? message = null)
    {
        return new()
        {
            Successful = serviceResponse.Successful,
            Message = message,
            Data = serviceResponse.Data
        };
    }
}