namespace CitMovie.Models.DomainObjects;

[Table("bookmark")]
public class Bookmark
{
    [Key]
    [Column("bookmark_id")]
    public int BookmarkId { get; set; }

    [Required, Column("user_id")]
    public required int UserId { get; set; }

    [Column("media_id")]
    public required int MediaId { get; set; }

    [Column("note")]
    public string? Note { get; set; }

    public User? User { get; set; }
}

