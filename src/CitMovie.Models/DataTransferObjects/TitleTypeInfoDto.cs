namespace CitMovie.Models.DataTransferObjects;

public record TitleTypeResult {
    public required int TitleTypeId { get; set; }
    public required string Name { get; set; }
}
