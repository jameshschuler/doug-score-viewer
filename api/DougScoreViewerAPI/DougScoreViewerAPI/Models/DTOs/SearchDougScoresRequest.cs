using System.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace DougScoreViewerAPI.Models.DTOs;

public record SearchDougScoresRequest(
    [FromQuery(Name = "make")]string? Make,
    [FromQuery(Name = "model")]string? Model,
    [FromQuery(Name = "year")]string? Year);