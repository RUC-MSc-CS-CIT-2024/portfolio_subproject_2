namespace CitMovie.Models.DomainObjects;

[Table("genre")]
public class Genre
{
    [Key, Column("genre_id")]
    public int GenreId { get; set; }
    [Column("name")]
    public required string Name { get; set; }
    
    public List<Media> Media { get; } = [];
}
