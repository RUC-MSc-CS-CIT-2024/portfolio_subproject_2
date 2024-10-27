namespace CitMovie.Models.DomainObjects;

[Table("score")]
public class Score
{
    [Key, Column("socre_id")]
    public int SocreId { get; set; }
    [Column("source")]
    public string Source { get; set; }
    [Column("value")]
    public string Value { get; set; }
    [Column("vote_count")]
    public int VoteCount { get; set; }
    [Column("at")]
    public DateTime At { get; set; }
    [Column("media_id")]
    public int MediaId { get; set; }
    [ForeignKey(nameof(MediaId))]
    public Media Media { get; set; }
}
