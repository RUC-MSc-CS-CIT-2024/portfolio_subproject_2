using CitMovie.Models.DomainObjects;

[Table("episode")]
public class Episode
{
    [Key, Column("episode_id")]
    public int EpisodeId { get; set; }

    [Column("episode_number")]
    public int EpisodeNumber { get; set; }

    [Column("season_id")]
    public int SeasonId { get; set; }

    [Column("media_id")]
    public int MediaId { get; set; }

    public Media Media { get; set; }

    [ForeignKey(nameof(SeasonId))]
    public Season Season { get; set; }
}