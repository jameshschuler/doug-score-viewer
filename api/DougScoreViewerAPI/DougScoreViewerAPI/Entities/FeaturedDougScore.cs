using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DougScoreViewerAPI.Entities;

[Table("featured_doug_score")]
public record FeaturedDougScore : BaseEntity
{
    [Column("created_at")]
    public DateTime CreatedAt { get; init; }
    
    [Column("date_featured")]
    public DateTime DateFeatured { get; init; }
    
    [Column("updated_at")]
    public DateTime? UpdateAt { get; init; }
    
    [Column("doug_score_id")]
    public int DougScoreId { get; init; }
    
    [JsonIgnore]
    public virtual DougScore? DougScore { get; init; }
}