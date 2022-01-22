using DougScoreViewerAPI.Entities;

namespace DougScoreViewerAPI.Models.DTOs;

public record DougScoreDto(FilmingLocationDto? FilmingLocation, Vehicle? Vehicle, DailyScore? DailyScore,
    WeekendScore? WeekendScore, string? VideoLink, int? TotalDougScore)
{ 
    public int Id { get; init; }
};