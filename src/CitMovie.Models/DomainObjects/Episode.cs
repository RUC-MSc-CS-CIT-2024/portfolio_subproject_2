namespace CitMovie.Models.DomainObjects;

[Table("esiode")]
public class Episode
{
    [Key, Column("media_id")]
    public int MediaId { get; set; }
    [Column("episode_number")]
    public int EpisodeNumber { get; set; }
    [Column("season_id")]
    public int SeasonId { get; set; }
    
    [NotMapped]
    [ForeignKey(nameof(MediaId))]
    public Media Media { get; set; }
    [ForeignKey(nameof(SeasonId))]
    public Media Season { get; set; }
}
