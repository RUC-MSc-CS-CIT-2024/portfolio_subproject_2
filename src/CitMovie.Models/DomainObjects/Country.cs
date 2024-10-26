namespace CitMovie.Models.DomainObjects;

[Table("country")]
public class Country
{
    [Key, Column("country_id")]
    public int CountryId { get; set; }
    [Column("imdb_country_code")]
    public string ImdbCountryCode { get; set; }
    [Column("iso_code")]
    public string IsoCode { get; set; }
    [Column("name")]
    public string Name { get; set; }

    public ICollection<Title> Titles { get; } = new List<Title>();
    public ICollection<Release> Releases { get; } = new List<Release>();

    public List<Media> Media { get; } = [];

}
