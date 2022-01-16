using System.ComponentModel.DataAnnotations.Schema;

namespace DougScoreViewerAPI.Entities;

[Table("doug_scores")]
public class DougScores
{
    [Column("id")]
    public int Id { get; init; }
    
    [Column("city")]
    public string? City { get; init; }
    
    [Column("state")]
    public string? State { get; init; }
    
    [Column("created_at")]
    public DateTime CreatedAt { get; init; }
    
    [Column("make")]
    public string? Make { get; init; }
    
    [Column("model")]
    public string? Model { get; init; }
    
    [Column("origin_country")]
    public string? OriginCountry { get; init; }
    
    [Column("total_doug_score")]
    public int? TotalDougScore { get; init; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; init; }
    
    [Column("video_link")]
    public string? VideoLink { get; init; }
    
    [Column("year")]
    public string? Year { get; init; }
}