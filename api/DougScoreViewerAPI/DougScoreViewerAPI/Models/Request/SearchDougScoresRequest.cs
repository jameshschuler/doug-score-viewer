using DougScoreViewerAPI.Models.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace DougScoreViewerAPI.Models.Request;

public record SearchDougScoresRequest(
    [FromQuery(Name = "make")]string? Make,
    [FromQuery(Name = "model")]string? Model,
    [FromQuery(Name = "year")]string? Year,
    [FromQuery(Name = "originCountry")] string? OriginCountry,
    [FromQuery(Name="sortBy")][SortBy] string SortBy = "TotalDougScore",
    [FromQuery(Name = "sortOrder")] string SortOrder = "desc",
    [FromQuery(Name = "page")]int Page = 1);
    