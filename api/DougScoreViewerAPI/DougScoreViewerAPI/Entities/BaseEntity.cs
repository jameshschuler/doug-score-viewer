using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DougScoreViewerAPI.Entities;

public record BaseEntity
{
    [Key, Column("id")]
    public int Id { get; init; }
}