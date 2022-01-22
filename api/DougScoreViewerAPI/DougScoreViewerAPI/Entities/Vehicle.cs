using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DougScoreViewerAPI.Entities;

[Table("vehicle")]
public record Vehicle : BaseEntity
{
    [Column("make")]
    public string? Make { get; init; }
    
    [Column("model")]
    public string? Model { get; init; }
    
    [Column("origin_country")]
    public string? OriginCountry { get; init; }
    
    [Column("year")]
    public string? Year { get; init; }
    
    [JsonIgnore]
    public virtual DougScore? DougScore { get; init; }
}