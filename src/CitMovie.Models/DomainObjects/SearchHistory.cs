using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CitMovie.Models.DomainObjects;


[Table("search_history")]
public class SearchHistory
{
    [Key]
    [Column("search_history_id")]
    public int SearchHistoryId { get; set; }

    [Required, Column("user_id")]
    public int UserId { get; set; }
    
    [ForeignKey("UserId")]
    public User User { get; set; }

    [Required, MaxLength(50), Column("type")]
    public string Type { get; set; }

    [Required, Column("query")]
    public string Query { get; set; }
}

