namespace CitMovie.Models.DomainObjects;

[Table("genre")]
public class Genre
{
    [Key, Column("genre_id")]
    public required int GenreId { get; set; }
    [Column("name")]
    public required string Name { get; set; }
    
    public ICollection<Media> Media { get; } = new List<Media>();
}
