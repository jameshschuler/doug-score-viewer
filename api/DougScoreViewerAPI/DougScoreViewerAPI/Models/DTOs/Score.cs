namespace DougScoreViewerAPI.Models.DTOs;

public record DailyScoreDto(int Features, int Comfort, int Quality, int Practical, int Value, int Total);

public record WeekendScoreDto(int Styling, int Acceleration, int Handling, int FunFactor, int CoolFactor, int Total);