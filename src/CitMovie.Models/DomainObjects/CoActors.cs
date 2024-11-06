namespace CitMovie.Models.DomainObjects;

[Keyless]
public class CoActor
{
    [Column("coactor_name")]
    public required string CoActorName { get; set; }
    [Column("coactor_imdb_id")]
    public required string CoActorImdbId { get; set; }
    [Column("frequency")]
    public required int Frequency { get; set; }
}
