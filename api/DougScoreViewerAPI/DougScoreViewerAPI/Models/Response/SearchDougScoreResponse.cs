namespace DougScoreViewerAPI.Models.Response;

public record SearchDougScoreResponse(IEnumerable<DougScoreResponse> DougScores, int CurrentCount, int TotalCount, int CurrentPage, int TotalPageCount);