using System.ComponentModel.DataAnnotations.Schema;

namespace DougScoreViewerAPI.Entities;

[Table("daily_score")]
public class DailyScore : BaseEntity
{
    [Column("comfort")]
    public int? Comfort { get; init; }
    
    [Column("features")]
    public int? Features { get; init; }
    
    [Column("practicality")]
    public int? Practicality { get; init; }
    
    [Column("quality")]
    public int? Quality { get; init; }
    
    [Column("value")]
    public int? Value { get; init; }
    
    [Column("total")]
    public int? Total { get; init; }
    
    public virtual DougScore? DougScore { get; init; }
}
