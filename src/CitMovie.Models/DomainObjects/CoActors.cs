namespace CitMovie.Models.DomainObjects
{
    [Keyless]
    public class CoActor
    {
        [Key, Column("coactor_name")]
        public string CoActorName { get; set; }
        [Key, Column("coactor_imdb_id")]
        public string CoActorImdbId { get; set; }
        [Key, Column("frequency")]
        public int Frequency { get; set; }
    }
}