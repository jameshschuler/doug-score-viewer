using DougScoreViewerAPI.Entities;

namespace DougScoreViewerAPI.Models.Response;

public record DougScoreResponse(FilmingLocation? FilmingLocation, Vehicle? Vehicle, DailyScore? DailyScore,
    WeekendScore? WeekendScore, string? VideoLink, int? TotalDougScore)
{ 
    public int Id { get; init; }
};