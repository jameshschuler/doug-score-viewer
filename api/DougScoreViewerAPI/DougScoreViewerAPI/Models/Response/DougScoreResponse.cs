using DougScoreViewerAPI.Models.DTOs;

namespace DougScoreViewerAPI.Models.Response;

public record DougScoreResponse(IEnumerable<DougScoreDto> DougScores, int TotalCount);