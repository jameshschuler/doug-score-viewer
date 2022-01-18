using System.ComponentModel.DataAnnotations.Schema;

namespace DougScoreViewerAPI.Entities;

[Table("vehicle")]
public class Vehicle : BaseEntity
{
    [Column("make")]
    public string? Make { get; init; }
    
    [Column("model")]
    public string? Model { get; init; }
    
    [Column("origin_country")]
    public string? OriginCountry { get; init; }
    
    [Column("year")]
    public string? Year { get; init; }
    
    public virtual DougScore? DougScore { get; init; }
}