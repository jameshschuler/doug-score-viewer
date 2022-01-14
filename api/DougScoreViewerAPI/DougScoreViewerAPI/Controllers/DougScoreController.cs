using DougScoreViewerAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DougScoreViewerAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DougScoreController : BaseController<DougScoreController>
{
    private readonly ILogger<DougScoreController> _logger;

    public DougScoreController(ILogger<DougScoreController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public ActionResult<ApiResponse<DougScoreResponse>> Search()
    {
        var t = new List<DougScore>()
        {
            new DougScore(null, null, null, null, null, 0)
        };
        var x = new DougScoreResponse(t);
        var response = new ServiceResponse<DougScoreResponse>()
        {
            Data = x
        };

        return HandleResponse(response, null, "Doug Scores");
    }
    
    [HttpPost]
    public IActionResult Sync()
    {
        return Ok();
    }
}