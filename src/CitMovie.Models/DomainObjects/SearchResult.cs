namespace CitMovie.Models.DomainObjects;

public class MediaSearchResult
{
    [Column("media_id")]
    public required int Id { get; set; }
    [Column("title")]
    public required string Title { get; set; }
}

public class PersonSearchResult
{
    [Column("person_id")]
    public required int Id { get; set; }
    [Column("name")]
    public required string Name { get; set; }
}
