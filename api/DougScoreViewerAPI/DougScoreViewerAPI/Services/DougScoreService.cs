using System.Diagnostics;
using AutoMapper;
using ClosedXML.Excel;
using DougScoreViewerAPI.Entities;
using DougScoreViewerAPI.Enums;
using DougScoreViewerAPI.Models;

namespace DougScoreViewerAPI.Services;

public interface IDougScoreService
{
    ServiceResponse<DougScoreResponse>  GetDougScores();
    void SyncDougScores();
}

public class DougScoreService : IDougScoreService
{
    private readonly IWebHostEnvironment _environment;
    private readonly IMapper _mapper;
    private readonly MyContext _context;

    public DougScoreService(IWebHostEnvironment environment, IMapper mapper, MyContext context)
    {
        _environment = environment;
        _mapper = mapper;
        _context = context;
    }

    public ServiceResponse<DougScoreResponse> GetDougScores()
    {
        try
        {
            var dougScores = _context.DougScores!.ToList();
            var converted = dougScores.Select(e => 
                new DougScore(
                    new FilmLocation(e.City, e.State), 
                    new Vehicle(e.Make, e.Model, e.Year, e.OriginCountry),
                    null, null, e.VideoLink, e.TotalDougScore ?? 0)
                {
                    Id = e.Id
                });
            
            var response = new ServiceResponse<DougScoreResponse>()
            {
                Data = new DougScoreResponse(converted)
            };

            return response;
        }
        catch (Exception)
        {
            return new ServiceResponse<DougScoreResponse>()
            {
                ErrorCode = ServiceErrorCode.BadRequest
            };
        }
    }
    
    public void SyncDougScores()
    {
        var path = Path.Combine(_environment.ContentRootPath, "Data", "DougScore.xlsx");
        var wb = new XLWorkbook(path);

        var ws = wb.Worksheet("DougScore");
        var lastRowUsed = ws.LastRowUsed();
        var rows = ws.Rows(4, lastRowUsed.RowNumber());

        var dougScores = new List<DougScore>();
        foreach (var row in rows)
        {
            var dougScore = new DougScore(
                new FilmLocation(row.Cell(18).GetString(), row.Cell(19).GetString()), 
                new Vehicle(
                    row.Cell(2).GetString(),
                    row.Cell(3).GetString(), 
                    row.Cell(1).GetString(), 
                    row.Cell(20).GetString()), 
                new DailyScore(
                    row.Cell(10).GetValue<int>(),
                    row.Cell(11).GetValue<int>(),
                    row.Cell(12).GetValue<int>(),
                    row.Cell(13).GetValue<int>(),
                    row.Cell(14).GetValue<int>(),
                    row.Cell(15).GetValue<int>()), 
                new WeekendScore(
                    row.Cell(4).GetValue<int>(),
                    row.Cell(5).GetValue<int>(),
                    row.Cell(6).GetValue<int>(),
                    row.Cell(7).GetValue<int>(),
                    row.Cell(8).GetValue<int>(),
                    row.Cell(9).GetValue<int>()), 
                row.Cell(17).GetString(), row.Cell(16).GetValue<int>());
            
            dougScores.Add(dougScore);
        }

        Debug.Print(dougScores.Count().ToString());
        // save data into database
    } 
}