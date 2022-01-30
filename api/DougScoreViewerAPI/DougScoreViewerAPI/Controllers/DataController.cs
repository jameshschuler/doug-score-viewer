using System.ComponentModel.DataAnnotations;
using DougScoreViewerAPI.Models;
using DougScoreViewerAPI.Models.Response;
using DougScoreViewerAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace DougScoreViewerAPI.Controllers;

[ApiController]
[Route("api/v1/data")]
public class DataController : BaseController<DougScoreController>
{
    private readonly IDataService _dataService;

    public DataController(IDataService dataService)
    {
        _dataService = dataService;
    }
    
    [HttpGet("makes")]
    public ActionResult<ApiResponse<AvailableMakesResponse>> GetAvailableMakes()
    {
        var response = _dataService.GetAvailableMakes();
        return HandleResponse(response);
    }

    [HttpGet("models")]
    public ActionResult<ApiResponse<AvailableModelsResponse>> GetAvailableModels([FromQuery][Required]string make)
    {
        var response = _dataService.GetAvailableModels(make);
        return HandleResponse(response);
    }
}