namespace CitMovie.Models.DomainObjects;

[Table("collection")]
public class Collection
{
    [Key, Column("collection_id")]
    public int CollectionId { get; set; }
    [Column("name")]
    public string Name { get; set; }
    [Column("description")]
    public string? Description { get; set; }

    public List<Media> Media { get; } = [];
}
