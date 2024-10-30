namespace CitMovie.Models.DomainObjects;

[Table("title")]
public class Title
{
    [Key, Column("title_id")]
    public int TitleId { get; set; }

    [Column("name")]
    public required string Name { get; set; }

    [Column("media_id")]
    public int MediaId { get; set; }

    [Column("country_id")]
    public int? CountryId { get; set; }

    [Column("language_id")]
    public int? LanguageId { get; set; }
    
    public Media? Media { get; set; }
    public Country Country { get; set; } = null!;
    public Language? Language { get; set; }
    
    public ICollection<Release> Releases { get; } = new List<Release>();
 
    public List<TitleType> TitleTypes { get; } = [];
    public List<TitleAttribute> TitleAttributes { get; } = [];
}
