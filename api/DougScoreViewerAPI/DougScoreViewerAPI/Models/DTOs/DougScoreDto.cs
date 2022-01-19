namespace DougScoreViewerAPI.Models.DTOs;

public record DougScoreDto(FilmingLocationDto? FilmingLocation, VehicleDto? Vehicle, DailyScoreDto? DailyScore,
    WeekendScoreDto? WeekendScore, string? VideoLink, int? TotalDougScore)
{ 
    public int Id { get; init; }
};