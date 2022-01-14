namespace DougScoreViewerAPI.Models;

public record Score(int Total)
{
    
}

public record DailyScore(int Features, int Comfort, int Quality, int Practical, int Value, int Total) : Score(Total)
{
    public new int Total => Features + Comfort + Quality + Practical + Value;
}

public record WeekendScore
    (int Styling, int Acceleration, int Handling, int FunFactor, int CoolFactor, int Total) : Score(Total)
{
    public new int Total => Styling + Acceleration + Handling + FunFactor + CoolFactor;
}