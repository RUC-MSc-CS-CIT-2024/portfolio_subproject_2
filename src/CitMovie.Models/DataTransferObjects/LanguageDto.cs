namespace CitMovie.Models.DataTransferObjects;

public record LanguageResult {
    public required int LanguageId { get; set; }
    public required string Name { get; set; }
    public required string IsoCode { get; set; }
}
