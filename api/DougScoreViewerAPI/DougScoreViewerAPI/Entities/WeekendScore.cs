using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DougScoreViewerAPI.Entities;

[Table("weekend_score")]
public record WeekendScore : BaseEntity
{
    [Column("acceleration")]
    public int? Acceleration { get; init; }
    
    [Column("cool_factor")]
    public int? CoolFactor { get; init; }
    
    [Column("fun_factor")]
    public int? FunFactor { get; init; }
    
    [Column("handling")]
    public int? Handling { get; init; }
    
    [Column("styling")]
    public int? Styling { get; init; }
    
    [Column("total")]
    public int? Total { get; init; }
    
    [JsonIgnore]
    public virtual DougScore? DougScore { get; init; }
}