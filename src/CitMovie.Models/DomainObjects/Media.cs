namespace CitMovie.Models.DomainObjects;

[Table("media")]
public class Media
{
    [Key, Column("media_id")]
    public int Id { get; set; }

    [Column("type")]
    public required string Type { get; set; }

    [Column("plot")]
    public string? Plot { get; set; }

    [Column("runtime")]
    public int? Runtime { get; set; }

    [Column("box_office")]
    public int? BoxOffice { get; set; }

    [Column("budget")]
    public int? Budget { get; set; }

    [Column("imdb_id")]
    public string? ImdbId { get; set; }

    [Column("awards")]
    public string? Awards { get; set; }

    public MediaPrimaryInformation? PrimaryInformation { get; set; }

    public List<MediaProductionCompany> MediaProductionCompany { get; } = [];
    
    public List<Score> Scores { get; } = [];
    public List<Release> Releases { get; } = [];
    public List<Title> Titles { get; } = [];
    public List<RelatedMedia> RelatedMedia { get; } = [];
    public List<CastMember> CastMembers { get; } = [];
    public List<CrewMember> CrewMembers { get; } = [];

    public List<MediaCollection> Collections { get; } = [];
    public List<Country> Countries { get; } = [];
    public List<Genre> Genres { get; } = [];

}
