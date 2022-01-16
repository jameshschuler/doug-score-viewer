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
    public IActionResult Sync()
    {
        _logger.LogDebug("Sync...");
        _dougScoreService.SyncDougScores();
        return Ok(new { message = "Hello" });
    }
}