namespace DougScoreViewerAPI.Models;

public record DougScore(FilmLocation? FilmLocation, Vehicle? Vehicle, DailyScore? DailyScore,
    WeekendScore? WeekendScore, string? VideoLink, int TotalDougScore)
{ 
    public int Id { get; set; }

    public DateTime CreatedAt { get; set; }
    
    public DateTime? UpdatedAt { get; set; }
};