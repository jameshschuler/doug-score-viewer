using System.ComponentModel.DataAnnotations;
using DougScoreViewerAPI.Models.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace DougScoreViewerAPI.Models.Request;

public record SearchDougScoresRequest(
    [FromQuery(Name = "make")]string? Make,
    [FromQuery(Name = "model")]string? Model,
    [FromQuery(Name = "minYear")][Range(1900, 2050)]int? MinYear,
    [FromQuery(Name = "maxYear")][Range(1900, 2050)][GreaterThan]int? MaxYear,
    [FromQuery(Name = "originCountries")] string? OriginCountries,
    [FromQuery(Name="sortBy")][SortBy] string SortBy = "TotalDougScore",
    [FromQuery(Name = "sortOrder")] string SortOrder = "desc",
    [FromQuery(Name = "page")]int Page = 1,
    [FromQuery(Name = "pageSize")][Range(1, 25)] int PageSize = 5);
    