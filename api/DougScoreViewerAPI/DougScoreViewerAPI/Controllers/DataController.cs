using DougScoreViewerAPI.Enums;
using DougScoreViewerAPI.Models;
using DougScoreViewerAPI.Models.Response;
using DougScoreViewerAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace DougScoreViewerAPI.Controllers;

[ApiController]
[Route("api/v1/data")]
public class DataController : BaseController<DougScoreController>
{
    private readonly ILogger<DougScoreController> _logger;
    private readonly IDataService _dataService;

    public DataController(ILogger<DougScoreController> logger, IDataService dataService)
    {
        _logger = logger;
        _dataService = dataService;
    }
    
    [HttpGet("makes")]
    public ActionResult<ApiResponse<AvailableMakesResponse>> GetAvailableMakes()
    {
        try
        {
            var response = _dataService.GetAvailableMakes();

            return HandleResponse(response, null, null);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error occurred while retrieving available makes...{errorMessage}", ex.Message);
            return HandleResponse(
                new ServiceResponse<AvailableMakesResponse>()
                {
                    ErrorCode = ServiceErrorCode.InternalServer
                }, "Error occurred while retrieving available makes! Please try again.", null);
        }
    }
}