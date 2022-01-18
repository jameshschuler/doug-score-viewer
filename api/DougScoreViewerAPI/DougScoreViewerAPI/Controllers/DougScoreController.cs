using DougScoreViewerAPI.Enums;
using DougScoreViewerAPI.Extensions;
using DougScoreViewerAPI.Models;
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

    [HttpGet]
    public ActionResult<ApiResponse<DougScoreResponse>> Search()
    {
        var response = _dougScoreService.GetDougScores();

        return HandleResponse(response, null, "Doug Scores");
    }
    
    [HttpPost("sync")]
    public async Task<ActionResult<ApiResponse<SyncDougScoresResponse>>> Sync()
    {
        try
        {
            var response = await _dougScoreService.SyncDougScores();
            return HandleResponse(response, null, "Doug Scores");
        }
        catch (Exception ex)
        {
            _logger.LogError("Error: ${message}", ex.Message);
            
            return HandleResponse(new ServiceResponse<SyncDougScoresResponse>()
            {
                ErrorCode = ServiceErrorCode.InternalServer
            }, "Unable to sync Doug Scores.", "Doug Scores");
        }
    }
}