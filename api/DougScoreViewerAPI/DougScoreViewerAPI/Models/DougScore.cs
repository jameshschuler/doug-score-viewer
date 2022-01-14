namespace DougScoreViewerAPI.Models;

public record DougScore(FilmLocation? FilmLocation, Vehicle? Vehicle, DailyScore? DailyScore, WeekendScore? WeekendScore, string? VideoLink, int TotalDougScore);