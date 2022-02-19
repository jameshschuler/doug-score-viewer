using System.ComponentModel.DataAnnotations;
using DougScoreViewerAPI.Models;
using DougScoreViewerAPI.Models.Request;
using DougScoreViewerAPI.Models.Response;
using DougScoreViewerAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace DougScoreViewerAPI.Controllers;

[ApiController]
[Route("api/v1/dougscore")]
public class DougScoreController : BaseController<DougScoreController>
{
    private readonly IDougScoreService _dougScoreService;

    public DougScoreController(IDougScoreService dougScoreService)
    {
        _dougScoreService = dougScoreService;
    }

    [HttpGet("search")]
    public ActionResult<ApiResponse<SearchDougScoreResponse>> Search([FromQuery]SearchDougScoresRequest request)
    { 
        var response = _dougScoreService.SearchDougScores(request);
        return HandleResponse(response);
    }
    
    [HttpGet("featured")]
    public async Task<ActionResult<ApiResponse<GetDougScoresResponse>>> Featured()
    { 
        var response = await _dougScoreService.GetFeatured();
        return HandleResponse(response);
    }

    [HttpGet("{dougScoreId:int:min(1)}")]
    public ActionResult<ApiResponse<GetDougScoreResponse>> Get([Required]int dougScoreId)
    {
        var response = _dougScoreService.GetDougScore(dougScoreId);
        return HandleResponse(response);
    }

    [HttpPost("setFeatured")]
    public async Task<ActionResult<ApiResponse<SetFeaturedDougScoresResponse>>> SetFeaturedDougScores()
    {
        var response = await _dougScoreService.SetFeaturedDougScores();
        return HandleResponse(response);
    }

    [HttpPost("sync")]
    public async Task<ActionResult<ApiResponse<SyncDougScoresResponse>>> Sync()
    {
        var response = await _dougScoreService.SyncDougScores();
        return HandleResponse(response);
    }
}