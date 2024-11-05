namespace CitMovie.Models.DataTransferObjects;

public record CrewResult {
    public required int Id { get; set; }
    public required string Role { get; set; }
    public string? Character { get; set; }
    public required string JobCategory { get; set; }
    public required int PersonId { get; set; }
}
