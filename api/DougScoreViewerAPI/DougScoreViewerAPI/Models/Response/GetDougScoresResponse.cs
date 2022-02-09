namespace DougScoreViewerAPI.Models.Response;

public record GetDougScoresResponse(IEnumerable<DougScoreResponse> DougScores);
