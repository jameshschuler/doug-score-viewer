using DougScoreViewerAPI.Enums;
using DougScoreViewerAPI.Models;
using DougScoreViewerAPI.Models.DTOs;
using DougScoreViewerAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace DougScoreViewerAPI.Controllers;

[ApiController]
[Route("api/v1/dougscore")]
public class DougScoreController : BaseController<DougScoreController>
{
    private readonly ILogger<DougScoreController> _logger;
    private readonly IDougScoreService _dougScoreService;

    public DougScoreController(ILogger<DougScoreController> logger, IDougScoreService dougScoreService)
    {
        _logger = logger;
        _dougScoreService = dougScoreService;
    }

    [HttpGet("search")]
    public ActionResult<ApiResponse<DougScoreResponse>> Search([FromQuery]SearchDougScoresRequest request)
    {
        try
        {
            var response = _dougScoreService.SearchDougScores(request);

            return HandleResponse(response, null, "DougScore");
        }
        catch (Exception ex)
        {
            _logger.LogError("Error occurred while searching...{errorMessage}", ex.Message);
            return HandleResponse(
                new ServiceResponse<DougScoreResponse>()
                {
                    ErrorCode = ServiceErrorCode.InternalServer
                }, "Error occurred while searching! Please try again.", "DougScore");
        }
        
    }
    
    [HttpPost("sync")]
    public async Task<ActionResult<ApiResponse<SyncDougScoresResponse>>> Sync()
    {
        try
        {
            var response = await _dougScoreService.SyncDougScores();
            return HandleResponse(response, null, "DougScore");
        }
        catch (Exception ex)
        {
            _logger.LogError("Error: ${message}", ex.Message);
            
            return HandleResponse(new ServiceResponse<SyncDougScoresResponse>()
            {
                ErrorCode = ServiceErrorCode.InternalServer
            }, "Unable to sync Doug Scores.", "DougScore");
        }
    }
}