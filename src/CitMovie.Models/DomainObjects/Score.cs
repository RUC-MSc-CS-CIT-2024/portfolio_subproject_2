namespace CitMovie.Models.DomainObjects;

[Table("score")]
public class Score
{
    [Key, Column("score_id")]
    public int SocreId { get; set; }
    [Column("source")]
    public required string Source { get; set; }
    [Column("value")]
    public required string Value { get; set; }
    [Column("vote_count")]
    public int? VoteCount { get; set; }
    [Column("at")]
    public DateTime? At { get; set; }
    [Column("media_id")]
    public required int MediaId { get; set; }
    
    public Media? Media { get; set; }
}
