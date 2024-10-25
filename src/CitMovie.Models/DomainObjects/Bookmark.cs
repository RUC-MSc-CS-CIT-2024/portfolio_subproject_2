using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CitMovie.Models.DomainObjects;


[Table("bookmark")]
public class Bookmark
{
    [Key]
    [Column("bookmark_id")]
    public int BookmarkId { get; set; }

    [Required, Column("user_id")]
    public int UserId { get; set; }
    
    [ForeignKey("UserId")]
    public User User { get; set; }

    [Column("media_id")]
    public int MediaId { get; set; }

    [Column("note")]
    public string? Note { get; set; }
}

