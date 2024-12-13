namespace CitMovie.Models.DomainObjects;

[Table("episode")]
public class Episode : Media
{
    [Column("episode_number")]
    public int? EpisodeNumber { get; set; }
    
    [Column("season_id")]
    public required int SeasonId { get; set; }
    
    public Season? Season { get; set; }
}
