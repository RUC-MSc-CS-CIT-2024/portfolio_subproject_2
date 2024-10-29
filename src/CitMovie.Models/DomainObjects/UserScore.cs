using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CitMovie.Models.DomainObjects;


[Table("user_score")]
public class UserScore
{
    [Key]
    [Column("user_score_id")]
    public int UserScoreId { get; set; }

    [Required, Column("user_id")]
    public int UserId { get; set; }
    
    [ForeignKey("UserId")]
    public User User { get; set; }

    [Column("media_id")]
    public int MediaId { get; set; }

    [Required, Column("score_value")]
    [Range(1, 10)]
    public int ScoreValue { get; set; }

    [Column("review_text")]
    public string? ReviewText { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}

