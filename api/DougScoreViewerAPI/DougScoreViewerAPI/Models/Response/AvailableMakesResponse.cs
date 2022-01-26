using DougScoreViewerAPI.Models.DTOs;

namespace DougScoreViewerAPI.Models.Response;

public record AvailableMakesResponse(IEnumerable<AvailableMakeDto> AvailableMakes);