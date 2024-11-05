namespace CitMovie.Models.DataTransferObjects;

public record TitleResult {
    public int Id { get; set; }
    public required string Name { get; set; }
    public IEnumerable<TitleTypeResult> Types { get; set; } = [];
    public IEnumerable<TitleAttributeResult> Attributes { get; set; } = [];
    public CountryResult? Country { get; set; }
    public LanguageResult? Language { get; set; }
}

public record TitleCreateRequest {
    public required string Name { get; set; }
    public IEnumerable<int>? TypeIds { get; set; }
    public IEnumerable<int>? AttributeIds { get; set; }
    public int? CountryId { get; set; }
    public int? LanguageId { get; set; }
}
