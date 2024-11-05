namespace CitMovie.Models.DataTransferObjects;

public record LanguageResult {
    public int LanguageId { get; set; }
    public string Name { get; set; }
    public string IsoCode { get; set; }
}
