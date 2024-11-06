namespace CitMovie.Models.DomainObjects;

[Keyless]
public class CoActor
{
    [Key, Column("coactor_name")]
    public required string CoActorName { get; set; }
    [Key, Column("coactor_imdb_id")]
    public required string CoActorImdbId { get; set; }
    [Key, Column("frequency")]
    public required int Frequency { get; set; }
}
