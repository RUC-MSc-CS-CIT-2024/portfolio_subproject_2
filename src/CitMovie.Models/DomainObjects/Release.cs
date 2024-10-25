namespace CitMovie.Models.DomainObjects;
[Table("release")]
public class Release
{
    [Key, Column("release_id")]
    public int ReleaseId { get; set; }
    [Column("rated")]
    public string Rated { get; set; }
    [Column("type")]
    public string Type { get; set; }
    [Column("country_id")]
    public int CountryId { get; set; }
    [Column("media_id")]
    public int MediaId { get; set; }
    [Column("title_id")]
    public int TitleId { get; set; }

    [ForeignKey(nameof(CountryId))]
    public Country Country { get; set; }
    [ForeignKey(nameof(MediaId))]
    public Media Media { get; set; }
    [ForeignKey(nameof(TitleId))]
    public Title Title { get; set; }
    
    public ICollection<PromotionalMedia> PromotionalMedias { get; } = new List<PromotionalMedia>();
    
    public List<Language> Languages { get; } = [];

}
