namespace CitMovie.Models.DomainObjects;
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
    [Column("website")]
    public string? Website { get; set; }
    [Column("awards")]
    public string? Awards { get; set; }
    [Column("original_title_id")]
    public int OriginalTitleId { get; set; }
    [Column("primary_release_id")]
    public int PrimaryReleaseId { get; set; }
    
    
    public Title Title { get; set; }
    public Release Release { get; set; }
    public Season? Season { get; set; }
    public Episode? Episode { get; set; }
    public MediaProductionCompany? MediaProductionCompany { get; set; }
    public Score? Score { get; set; }
    
    public ICollection<RelatedMedia>? RelatedMedia { get; } = new List<RelatedMedia>();
    public ICollection<CastMember> CastMembers { get; } = new List<CastMember>();
    public ICollection<CrewMember> CrewMembers { get; } = new List<CrewMember>();

    public List<Collection> Collections { get; } = [];
    public List<Country> Countries { get; } = [];
    public List<Genre> Genres { get; } = [];

}
