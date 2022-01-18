using DougScoreViewerAPI.Models.DTOs;

namespace DougScoreViewerAPI.Models;

public record DougScoreResponse(IEnumerable<DougScoreDto> DougScores);