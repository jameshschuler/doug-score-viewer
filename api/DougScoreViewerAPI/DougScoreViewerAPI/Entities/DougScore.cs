using System.ComponentModel.DataAnnotations.Schema;
using ClosedXML.Excel;

namespace DougScoreViewerAPI.Entities;

[Table("doug_score")]
public class DougScore : BaseEntity
{
    [Column("daily_score_id")]
    public int? DailyScoreId { get; init; }
    
    [Column("city")]
    public string? City { get; init; }
    
    [Column("state")]
    public string? State { get; init; }

    [Column("total_doug_score")]
    public int? TotalDougScore { get; init; }
    
    [Column("vehicle_id")]
    public int? VehicleId { get; init; }
    
    [Column("video_link")]
    public string? VideoLink { get; init; }

    [Column("weekend_score_id")]
    public int? WeekendScoreId { get; init; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
    
    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }
    
    [Column("upload_id")]
    public string? UploadId { get; init; }
    
    [NotMapped]
    public IXLCell? CL { get; set; }

    //
    
    public virtual DailyScore? DailyScore { get; init; }
    
    public virtual WeekendScore? WeekendScore { get; init; }
    
    public virtual Vehicle? Vehicle { get; init; }
}