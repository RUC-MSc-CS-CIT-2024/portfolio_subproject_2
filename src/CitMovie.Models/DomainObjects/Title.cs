namespace CitMovie.Models.DomainObjects;

[Table("title")]
public class Title
{
    [Key, Column("title_id")]
    public int Id { get; set; }

    [Column("name")]
    public required string Name { get; set; }

    [Column("media_id")]
    public int MediaId { get; set; }

    [Column("country_id")]
    public int? CountryId { get; set; }

    [Column("language_id")]
    public int? LanguageId { get; set; }
    
    public Media? Media { get; set; }
    public Country? Country { get; set; }
    public Language? Language { get; set; }
    
    public List<Release> Releases { get; } = [];
 
    public List<TitleType> TitleTypes { get; } = [];
    public List<TitleAttribute> TitleAttributes { get; } = [];
}
