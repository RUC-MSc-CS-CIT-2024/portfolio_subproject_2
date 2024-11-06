namespace CitMovie.Models.DomainObjects;

[Table("collection")]
public class MediaCollection
{
    [Key, Column("collection_id")]
    public int CollectionId { get; set; }
    [Column("name")]
    public required string Name { get; set; }
    [Column("description")]
    public string? Description { get; set; }

    public List<Media> Media { get; } = [];
}
