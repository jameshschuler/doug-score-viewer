using System.Web;
using AutoMapper;
using ClosedXML.Excel;
using DougScoreViewerAPI.Entities;
using DougScoreViewerAPI.Models;
using DougScoreViewerAPI.Models.Request;
using DougScoreViewerAPI.Models.Response;
using Microsoft.EntityFrameworkCore;

namespace DougScoreViewerAPI.Services;

public interface IDougScoreService
{
    ServiceResponse<GetDougScoreResponse>  GetDougScore(int dougScoreId);
    Task<ServiceResponse<GetDougScoresResponse>> GetFeatured();
    ServiceResponse<SearchDougScoreResponse>  SearchDougScores(SearchDougScoresRequest request);

    Task<ServiceResponse<SetFeaturedDougScoresResponse>> SetFeaturedDougScores();
    Task<ServiceResponse<SyncDougScoresResponse>> SyncDougScores();
}

public class DougScoreService : IDougScoreService
{
    private readonly IWebHostEnvironment _environment;
    private readonly IMapper _mapper;
    private readonly MyContext _context;
    private readonly ILogger<DougScoreService> _logger;
    
    public DougScoreService(IWebHostEnvironment environment, IMapper mapper, MyContext context, ILogger<DougScoreService> logger)
    {
        _environment = environment;
        _mapper = mapper;
        _context = context;
        _logger = logger;
    }

    public ServiceResponse<GetDougScoreResponse> GetDougScore(int dougScoreId)
    {
        var dougScore = _context.DougScores!
            .Include(e => e.Vehicle)
            .Include(e => e.DailyScore)
            .Include(e => e.WeekendScore)
            .FirstOrDefault(e => e.Id == dougScoreId);

        if (dougScore is null)
        {
            throw new KeyNotFoundException("Unable to find matching DougScore");
        }

        // TODO: move repeated logic to method
        var dougScoreResponse = new DougScoreResponse(
            new FilmingLocation(dougScore!.City, dougScore.State),
            dougScore.Vehicle,
            dougScore.DailyScore,
            dougScore.WeekendScore,
            dougScore.VideoLink,
            dougScore.TotalDougScore)
        {
            Id = dougScore.Id
        };

        return new ServiceResponse<GetDougScoreResponse>()
        {
            Data = new GetDougScoreResponse(dougScoreResponse)
        };
    }

    public async Task<ServiceResponse<GetDougScoresResponse>> GetFeatured()
    {
        var utcNow = DateTime.UtcNow.Date;
        var featuredQuery = _context.FeaturedDougScores!
            .Include(e => e.DougScore).ThenInclude(e => e!.Vehicle)
            .Include(e => e.DougScore).ThenInclude(e => e!.DailyScore)
            .Include(e => e.DougScore).ThenInclude(e => e!.WeekendScore)
            .Where(e => DateTime.Compare(e.DateFeatured.ToUniversalTime().Date, utcNow) == 0);
        
        var featuredDougScores = await featuredQuery
            .Select(e => new DougScoreResponse(
                new FilmingLocation(e.DougScore!.City, e.DougScore.State),
                e.DougScore.Vehicle,
                e.DougScore.DailyScore,
                e.DougScore.WeekendScore,
                e.DougScore.VideoLink,
                e.DougScore.TotalDougScore)
                {
                    Id = e.DougScore.Id
                }).ToListAsync();
        
        return new ServiceResponse<GetDougScoresResponse>
        {
            Data = new GetDougScoresResponse(featuredDougScores)
        };
    }
    
    public ServiceResponse<SearchDougScoreResponse> SearchDougScores(SearchDougScoresRequest request)
    {
        var dougScoresQuery = _context.DougScores!
            .Include(e => e.Vehicle)
            .Include(e => e.DailyScore)
            .Include(e => e.WeekendScore)
            .AsQueryable();

        dougScoresQuery = FilterDougScores(request, dougScoresQuery);
        dougScoresQuery = OrderDougScores(request, dougScoresQuery);

        var dougScoreCount = dougScoresQuery.Count();
        dougScoresQuery = dougScoresQuery.Skip((request.Page - 1) * request.PageSize).Take(request.PageSize);
            
        var dougScores = dougScoresQuery.ToList().Select(e => 
            new DougScoreResponse(
                new FilmingLocation(e.City, e.State), 
                e.Vehicle,
                e.DailyScore,
                e.WeekendScore, 
                e.VideoLink, 
                e.TotalDougScore)
            {
                Id = e.Id
            }).ToList();

        return new ServiceResponse<SearchDougScoreResponse>()
        {
            Data = new SearchDougScoreResponse(
                dougScores, 
                dougScores.Count(),
                dougScoreCount, 
                request.Page, 
                (int)Math.Ceiling(dougScoreCount / (double)request.PageSize))
        };
    }

    public async Task<ServiceResponse<SetFeaturedDougScoresResponse>> SetFeaturedDougScores()
    {
        if (_context.FeaturedDougScores!.Any(e => DateTime.Compare(e.DateFeatured.Date, DateTime.Today) == 0))
        {
           throw new AppException($"Featured DougScores have already been set for {DateTime.Today.ToLongDateString()}");
        }
        
        var minId = await _context.DougScores!.MinAsync(e => e.Id);
        var maxId = await _context.DougScores!.MaxAsync(e => e.Id);
        var randomIds = GetRandomNumbersBetweenRange(minId, maxId);

        var dougScores = _context.DougScores!.Where(e => randomIds.Contains(e.Id));
        if (!dougScores.Any())
        {
            throw new AppException();
        }

        var featuredDougScores = dougScores.Select(e => new FeaturedDougScore
        {
            DougScoreId = e.Id,
            DateFeatured = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        });
        
        await _context.FeaturedDougScores!.AddRangeAsync(featuredDougScores);
        var createdCount = await _context.SaveChangesAsync();
        
        return new ServiceResponse<SetFeaturedDougScoresResponse>
        {
            Data = new SetFeaturedDougScoresResponse(createdCount == dougScores.Count())
        };
    }
    
    public async Task<ServiceResponse<SyncDougScoresResponse>> SyncDougScores()
    {
        _logger.LogInformation("SyncDougScores...");

        if (_context.DougScores!.Any())
        {
             throw new AppException("DougScores were already synced.");
        }

        var path = Path.Combine(_environment.ContentRootPath, "Data", "DougScore_12052022.xlsx");
        var wb = new XLWorkbook(path, XLEventTracking.Disabled);

        var ws = wb.Worksheet("DougScore");
        var lastRowUsed = ws.LastRowUsed();
        var rows = ws.Rows(4, lastRowUsed.RowNumber() - 1);
        
        _logger.LogInformation("Found {rowCount} rows...", rows.Count());
        
        var uploadId = Guid.NewGuid().ToString();
        var dougScores = rows.Select(row => new DougScore
        {
            City = row.Cell(18).GetString().Trim(),
            State = row.Cell(19).GetString().Trim(),
            VideoLink = GetVideoLink(row.Cell(17)).Trim(),
            TotalDougScore = row.Cell(16).GetValue<int?>(),
            UploadId = uploadId,
            CreatedAt = DateTime.UtcNow,
            Vehicle = new Vehicle
            {
                Make = row.Cell(2).GetString().Trim(),
                Model = row.Cell(3).GetString().Trim(),
                Year = int.TryParse(row.Cell(1).GetString().Trim(), out var year) ? year : -1,
                OriginCountry = row.Cell(20).GetString().Trim()
            },
            DailyScore = new DailyScore
            {
                Features = row.Cell(10).GetValue<int>(),
                Comfort = row.Cell(11).GetValue<int>(),
                Quality = row.Cell(12).GetValue<int>(),
                Practicality = row.Cell(13).GetValue<int>(),
                Value = row.Cell(14).GetValue<int>(),
                Total = row.Cell(15).GetValue<int>()
            },
            WeekendScore = new WeekendScore
            {
                Styling = row.Cell(4).GetValue<int>(),
                Acceleration = row.Cell(5).GetValue<int>(),
                Handling = row.Cell(6).GetValue<int>(),
                FunFactor = row.Cell(7).GetValue<int>(),
                CoolFactor = row.Cell(8).GetValue<int>(),
                Total = row.Cell(9).GetValue<int>()
            }
        }).ToList();
        
        var oldCount = 0;
        var dougScoresToAdd = new List<DougScore>();
        foreach (var dougScore in dougScores)
        {
            var existingMatch = await _context.Vehicles!.FirstOrDefaultAsync(v => v.Year == dougScore.Vehicle!.Year
                && v.Make!.Trim() == dougScore.Vehicle.Make
                && v.Model!.Trim() == dougScore.Vehicle.Model);

            if (existingMatch is null)
            {
                dougScoresToAdd.Add(dougScore);
            }
            else
            {
                oldCount++;
            }
            
        }

        _logger.LogInformation("Found {dougScoreCount} existing DougScores...", oldCount);
        _logger.LogInformation("Found {dougScoreCount} new DougScores...", dougScoresToAdd.Count);
        _logger.LogInformation("Saving {dougScoresToBeSaved} rows...", dougScoresToAdd.Count);

        await _context!.DougScores!.AddRangeAsync(dougScoresToAdd);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Saved DougScores!");

        return new ServiceResponse<SyncDougScoresResponse>
        {
            Data = new SyncDougScoresResponse(dougScoresToAdd.Count)
        };
    }

    private static int[] GetRandomNumbersBetweenRange(int min, int max, int times = 5)
    {
        var result = new int[times];
        var random = new Random();

        for (var i = 0; i < times; i++)
        {
            result[i] = random.Next(min, max + 1);
        }

        return result;
    }

    private static string GetVideoLink(IXLCell? videoLinkCell)
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

    private static IQueryable<DougScore> FilterDougScores(SearchDougScoresRequest request, IQueryable<DougScore> dougScoresQuery)
    {
        if (!string.IsNullOrWhiteSpace(request.Make))
        {
            dougScoresQuery = dougScoresQuery.Where(e => e.Vehicle!.Make == request.Make);
        }

        if (!string.IsNullOrWhiteSpace(request.Model))
        {
            dougScoresQuery = dougScoresQuery.Where(e => e.Vehicle!.Model == request.Model);
        }
        
        if (request.MinYear.HasValue)
        {
            var maxYear = request.MaxYear ?? DateTime.Now.Year;
            
            dougScoresQuery = dougScoresQuery
                .Where(e => e.Vehicle!.Year >= request.MinYear && e.Vehicle!.Year <= maxYear );
        }
        
        if (!string.IsNullOrWhiteSpace(request.OriginCountries))
        {
            var countries = request.OriginCountries.Split(",").Distinct();
            dougScoresQuery = dougScoresQuery.Where(e => countries.Contains(e.Vehicle!.OriginCountry));
        }

        return dougScoresQuery;
    }

    private static IQueryable<DougScore> OrderDougScores(SearchDougScoresRequest request, IQueryable<DougScore> dougScoresQuery)
    {
        return dougScoresQuery = request.SortBy switch
        {
            "TotalDougScore" => request.SortOrder == "asc" ? 
                dougScoresQuery.OrderBy(e => e.TotalDougScore) : dougScoresQuery.OrderByDescending(e => e.TotalDougScore),
            "Year" => request.SortOrder == "asc" ? 
                dougScoresQuery.OrderBy(e => e.Vehicle!.Year) : dougScoresQuery.OrderByDescending(e => e.Vehicle!.Year),
            "DailyScore" => request.SortOrder == "asc" ? 
                dougScoresQuery.OrderBy(e => e.DailyScore!.Total) : dougScoresQuery.OrderByDescending(e => e.DailyScore!.Total),
            "WeekendScore" => request.SortOrder == "asc" ? 
                dougScoresQuery.OrderBy(e => e.WeekendScore!.Total) : dougScoresQuery.OrderByDescending(e => e.WeekendScore!.Total),
            _ => dougScoresQuery   
        };
    }
}