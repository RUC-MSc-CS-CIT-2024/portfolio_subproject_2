namespace CitMovie.Models.DomainObjects;

public class MatchSearchResult
{
    [Column("media_id")]
    public required int Id { get; set; }
    [Column("title")]
    public required string Title { get; set; }
}
