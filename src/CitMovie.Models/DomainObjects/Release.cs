namespace CitMovie.Models.DomainObjects;
[Table("release")]
public class Release
{
    [Key, Column("release_id")]
    public int ReleaseId { get; set; }
    [Column("release_date")]
    public DateTime? ReleaseDate { get; set; }
    [Column("rated")]
    public string? Rated { get; set; }
    [Column("type")]
    public required string Type { get; set; }
    [Column("media_id")]
    public required int MediaId { get; set; }
    [Column("country_id")]
    public int? CountryId { get; set; }
    [Column("title_id")]
    public int? TitleId { get; set; }

    public Country? Country { get; set; }
    public Media? Media { get; set; }
    public Title? Title { get; set; }
    
    public List<PromotionalMedia> PromotionalMedias { get; } = [];
    
    public List<Language> SpokenLanguages { get; } = [];

}
