using CitMovie.Models.DomainObjects;

[Table("media")]
public class Media
{
    [Key, Column("media_id")]
    public int MediaId { get; set; }

    [Column("type")]
    public string Type { get; set; }

    [Column("plot")]
    public string? Plot { get; set; }

    [Column("runtime")]
    public string? Runtime { get; set; }

    [Column("box_office")]
    public string? BoxOffice { get; set; }

    [Column("budget")]
    public string? Budget { get; set; }

    [Column("imdb_id")]
    public string? ImdbId { get; set; }

    [Column("awards")]
    public string? Awards { get; set; }

    [Column("original_title_id")]
    public int OriginalTitleId { get; set; }

    [Column("primary_release_id")]
    public int PrimaryReleaseId { get; set; }

    public Title Title { get; set; }
    public Release Release { get; set; }
    [NotMapped]
    public Season? Season { get; set; }
    public Episode? Episode { get; set; }
    public MediaProductionCompany? MediaProductionCompany { get; set; }
    public Score? Score { get; set; }
    
    [NotMapped]
    public ICollection<RelatedMedia>? RelatedMedia { get; } = new List<RelatedMedia>();
    public ICollection<CastMember> CastMembers { get; } = new List<CastMember>();
    public ICollection<CrewMember> CrewMembers { get; } = new List<CrewMember>();

    public List<Collection> Collections { get; set; } = new List<Collection>();
    public List<Country> Countries { get; set; } = new List<Country>();
    public List<Genre> Genres { get; set; } = new List<Genre>();

    public ICollection<Episode> Episodes { get; set; } = new List<Episode>();
    public ICollection<Season> Seasons { get; set; } = new List<Season>();
}