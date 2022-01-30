namespace DougScoreViewerAPI.Models.Response;

public record SearchDougScoreResponse(IEnumerable<DougScoreResponse> DougScores, int TotalCount);