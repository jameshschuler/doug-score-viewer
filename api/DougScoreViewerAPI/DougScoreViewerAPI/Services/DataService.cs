using System.Text.RegularExpressions;
using DougScoreViewerAPI.Entities;
using DougScoreViewerAPI.Models;
using DougScoreViewerAPI.Models.DTOs;
using DougScoreViewerAPI.Models.Response;

namespace DougScoreViewerAPI.Services;

public interface IDataService
{
    ServiceResponse<AvailableMakesResponse> GetAvailableMakes();
    
    ServiceResponse<AvailableModelsResponse> GetAvailableModels(string make);
}

public class DataService : IDataService
{
    private readonly MyContext _context;
    private readonly ILogger<DougScoreService> _logger;

    public DataService(MyContext context, ILogger<DougScoreService> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    public ServiceResponse<AvailableMakesResponse> GetAvailableMakes()
    {
        _logger.LogInformation("Getting available makes...");
        var query = _context.Vehicles!
            .GroupBy(e => new {  Make = e.Make!.Trim() })
            .Select(e => new
            {
                Make = e.Key.Make,
                Count = e.Count()
            })
            .OrderBy(e => e.Make);


        var makes = query
            .Select(e => new AvailableMakeDto(e.Make!, e.Count));
        
        return new ServiceResponse<AvailableMakesResponse>()
        {
            Data = new AvailableMakesResponse(makes.ToList())
        };
    }

    public ServiceResponse<AvailableModelsResponse> GetAvailableModels(string make)
    {
        _logger.LogInformation($"Getting available models for {make}...");
        var query = _context.Vehicles!
            .Where(e => e.Make == make)
            .OrderBy(e => e.Model)
            .Select(e => new AvailableModelDto(e.Id, e.Model!));
        
        return new ServiceResponse<AvailableModelsResponse>()
        {
            Data = new AvailableModelsResponse(query.ToList())
        };
    }
}