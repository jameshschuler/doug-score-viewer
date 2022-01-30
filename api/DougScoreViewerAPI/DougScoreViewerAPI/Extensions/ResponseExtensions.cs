using DougScoreViewerAPI.Models;

namespace DougScoreViewerAPI.Extensions;

public static class ResponseExtensions
{
    public static ApiResponse<T> ToApiResponse<T>(this ServiceResponse<T> serviceResponse)
    {
        return new ApiResponse<T>
        {
            Data = serviceResponse.Data
        };
    }
}