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

    // [Column("original_title_id")]
    // public int OriginalTitleId { get; set; }

    // [Column("primary_release_id")]
    // public int PrimaryReleaseId { get; set; }

    public MediaProductionCompany? MediaProductionCompany { get; set; }
    
    public ICollection<Score> Score { get; } = new List<Score>();
    public ICollection<Release> Release { get; } = new List<Release>();
    public ICollection<Title> Title { get; } = new List<Title>();
    [NotMapped]
    public ICollection<Media> RelatedMedia { get; } = new List<Media>();
    public ICollection<CastMember> CastMembers { get; } = new List<CastMember>();
    public ICollection<CrewMember> CrewMembers { get; } = new List<CrewMember>();

    public ICollection<MediaCollection> Collections { get; } = new List<MediaCollection>();
    public ICollection<Country> Countries { get; } = new List<Country>();
    public ICollection<Genre> Genres { get; } = new List<Genre>();

}
