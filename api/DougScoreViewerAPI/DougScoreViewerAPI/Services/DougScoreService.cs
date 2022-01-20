using System.Web;
using AutoMapper;
using ClosedXML.Excel;
using DougScoreViewerAPI.Entities;
using DougScoreViewerAPI.Models;
using DougScoreViewerAPI.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DougScoreViewerAPI.Services;

public interface IDougScoreService
{
    ServiceResponse<DougScoreResponse>  SearchDougScores(SearchDougScoresRequest request);
    Task<ServiceResponse<SyncDougScoresResponse>> SyncDougScores();
}

public class DougScoreService : IDougScoreService
{
    private readonly IWebHostEnvironment _environment;
    private readonly IMapper _mapper;
    private readonly MyContext _context;
    private readonly ILogger<DougScoreService> _logger;

    private const int PageLimit = 15;

    public DougScoreService(IWebHostEnvironment environment, IMapper mapper, MyContext context, ILogger<DougScoreService> logger)
    {
        _environment = environment;
        _mapper = mapper;
        _context = context;
        _logger = logger;
    }

    public ServiceResponse<DougScoreResponse> SearchDougScores(SearchDougScoresRequest request)
    {
        var dougScoresQuery = _context.DougScores!
            .Include(e => e.Vehicle)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Make))
        {
            dougScoresQuery = dougScoresQuery.Where(e => e.Vehicle!.Make == request.Make);
        }

        if (!string.IsNullOrWhiteSpace(request.Model))
        {
            dougScoresQuery = dougScoresQuery.Where(e => e.Vehicle!.Model == request.Model);
        }
        
        if (!string.IsNullOrWhiteSpace(request.Year))
        {
            dougScoresQuery = dougScoresQuery.Where(e => e.Vehicle!.Year == request.Year);
        }
        
        dougScoresQuery = dougScoresQuery
            .OrderBy(e => e.TotalDougScore)
            .Take(PageLimit);
            
        var dougScoreDtos = dougScoresQuery.ToList().Select(e => 
            new DougScoreDto(
                new FilmingLocationDto(e.City, e.State), 
                new VehicleDto(e.Vehicle?.Make, e.Vehicle?.Model, e.Vehicle?.Year, e.Vehicle?.OriginCountry),
                null,
                null, 
                e.VideoLink, 
                e.TotalDougScore)
            {
                Id = e.Id
            }).ToList();

        return new ServiceResponse<DougScoreResponse>()
        {
            Data = new DougScoreResponse(dougScoreDtos, dougScoreDtos.Count)
        };
    }
    
    public async Task<ServiceResponse<SyncDougScoresResponse>> SyncDougScores()
    {
        _logger.LogInformation("SyncDougScores...");

        var path = Path.Combine(_environment.ContentRootPath, "Data", "DougScore.xlsx");
        var wb = new XLWorkbook(path, XLEventTracking.Disabled);

        var ws = wb.Worksheet("DougScore");
        var lastRowUsed = ws.LastRowUsed();
        var rows = ws.Rows(4, lastRowUsed.RowNumber() - 1);
        
        _logger.LogInformation("Found {rowCount} rows...", rows.Count());
        
        var uploadId = Guid.NewGuid().ToString();
        var dougScores = rows.Select(row => new DougScore()
        {
            City = row.Cell(18).GetString(),
            State = row.Cell(19).GetString(),
            VideoLink = GetVideoLink(row.Cell(17)),
            TotalDougScore = row.Cell(16).GetValue<int?>(),
            UploadId = uploadId,
            CreatedAt = DateTime.UtcNow,
            Vehicle = new Vehicle()
            {
                Make = row.Cell(2).GetString(),
                Model = row.Cell(3).GetString(),
                Year = row.Cell(1).GetString(),
                OriginCountry = row.Cell(20).GetString()
            },
            DailyScore = new DailyScore()
            {
                Features = row.Cell(10).GetValue<int>(),
                Comfort = row.Cell(11).GetValue<int>(),
                Quality = row.Cell(12).GetValue<int>(),
                Practicality = row.Cell(13).GetValue<int>(),
                Value = row.Cell(14).GetValue<int>(),
                Total = row.Cell(15).GetValue<int>()
            },
            WeekendScore = new WeekendScore()
            {
                Styling = row.Cell(4).GetValue<int>(),
                Acceleration = row.Cell(5).GetValue<int>(),
                Handling = row.Cell(6).GetValue<int>(),
                FunFactor = row.Cell(7).GetValue<int>(),
                CoolFactor = row.Cell(8).GetValue<int>(),
                Total = row.Cell(9).GetValue<int>()
            }
        }).ToList();

        _logger.LogInformation("Saving {dougScoresToBeSaved} rows...", dougScores.Count);

        await _context!.DougScores!.AddRangeAsync(dougScores);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Saved DougScores!");

        return new ServiceResponse<SyncDougScoresResponse>
        {
            Data = new SyncDougScoresResponse(dougScores.Count)
        };
    }

    private string GetVideoLink(IXLCell? videoLinkCell)
    {
        if (videoLinkCell is null)
        {
            return string.Empty;
        }

        string videoLink;
        if (videoLinkCell.HasHyperlink)
        {
            videoLink = videoLinkCell.Hyperlink!.ExternalAddress?.OriginalString ?? string.Empty;
        }
        else
        {
            var hyperlink = videoLinkCell.Value as XLHyperlink;
            if (hyperlink is null)
            {
                return "n/a";
            }
            
            videoLink = hyperlink.ExternalAddress.OriginalString;
        }

        var urlParams = HttpUtility.ParseQueryString(videoLink);
        urlParams.Remove("t");
        
        return urlParams.ToString() ?? videoLink;
    }
}