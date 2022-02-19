namespace DougScoreViewerAPI.Models.Response;

public record GetDougScoresResponse(IEnumerable<DougScoreResponse> DougScores)
{
    public int Count => DougScores?.Count() ?? 0;
}

