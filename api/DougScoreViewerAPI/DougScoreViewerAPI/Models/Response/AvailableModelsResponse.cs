using DougScoreViewerAPI.Models.DTOs;

namespace DougScoreViewerAPI.Models.Response;

public record AvailableModelsResponse(IEnumerable<AvailableModelDto> Models);