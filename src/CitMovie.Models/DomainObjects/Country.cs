namespace CitMovie.Models.DomainObjects;

[Table("country")]
public class Country
{
    [Key, Column("country_id")]
    public int CountryId { get; set; }
    [Column("imdb_country_code")]
    public string? ImdbCountryCode { get; set; }
    [Column("iso_code")]
    public string? IsoCode { get; set; }
    [Column("name")]
    public required string Name { get; set; }

    public List<Title> Titles { get; } = [];
    public List<Release> Releases { get; } = [];
    public List<Media> Media { get; } = [];

}
