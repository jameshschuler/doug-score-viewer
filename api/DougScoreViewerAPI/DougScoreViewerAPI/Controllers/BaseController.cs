using DougScoreViewerAPI.Enums;
using DougScoreViewerAPI.Extensions;
using DougScoreViewerAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DougScoreViewerAPI.Controllers;

public class BaseController<TController> : ControllerBase
{
    protected ActionResult<ApiResponse<T>> HandleResponse<T>(ServiceResponse<T> serviceResponse, string? errorMessage, string? entity)
    {
        if (serviceResponse.Successful)
        {
            return Ok(serviceResponse.ToApiResponse());
        }

        var error = serviceResponse.ErrorCode ?? ServiceErrorCode.InternalServer;

        return error switch
        {
            ServiceErrorCode.BadRequest => BadRequest(serviceResponse.ToApiResponse(errorMessage)),
            ServiceErrorCode.NotFound => NotFound(serviceResponse.ToApiResponse($"No {entity} found.")),
            _ => StatusCode(500,serviceResponse.ToApiResponse(errorMessage))
        };

    }
}