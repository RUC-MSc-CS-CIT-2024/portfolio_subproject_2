namespace CitMovie.Models.DomainObjects;
[Table("release")]
public class Release
{
    [Key, Column("release_id")]
    public int ReleaseId { get; set; }
    [Column("release_date")]
    public DateTime ReleaseDate { get; set; }
    [Column("rated")]
    public string? Rated { get; set; }
    [Column("type")]
    public string Type { get; set; }
    [ForeignKey("Country")]
    [Column("country_id")]
    public int? CountryId { get; set; }
    [ForeignKey("Media")]
    [Column("media_id")]
    public int MediaId { get; set; }
    [ForeignKey("Title")]
    [Column("title_id")]
    public int? TitleId { get; set; }

    public Country? Country { get; set; }
    public Media Media { get; set; } = null!;
    public Title? Title { get; set; }
    
    public ICollection<PromotionalMedia> PromotionalMedias { get; set; } = new List<PromotionalMedia>();
    
    public List<Language> SpokenLanguages { get; set; } = [];

}
