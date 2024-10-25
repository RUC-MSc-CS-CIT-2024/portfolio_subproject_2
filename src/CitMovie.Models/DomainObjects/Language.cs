namespace CitMovie.Models.DomainObjects;

[Table("language")]
public class Language
{
    [Key, Column("language_id")]
    public int LanguageId { get; set; }
    [Column("imdb_language_code")]
    public string ImdbLanguageCode { get; set; }
    [Column("iso_code")]
    public string IsoCode { get; set; }
    [Column("name")]
    public string Name { get; set; }

    public List<Release> Releases { get; } = [];
}
