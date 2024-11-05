namespace CitMovie.Models.DataTransferObjects;

public class CountryResult {
    public required int CountryId { get; set; }
    public required string IsoCode { get; set; }
    public required string Name { get; set; }
}
