using DougScoreViewerAPI.Extensions;
using DougScoreViewerAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DougScoreViewerAPI.Controllers;

public class BaseController<TController> : ControllerBase
{
    protected ActionResult<ApiResponse<T>> HandleResponse<T>(ServiceResponse<T> serviceResponse)
    {
        return Ok(serviceResponse.ToApiResponse());
    }
}