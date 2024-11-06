namespace CitMovie.Models.DataTransferObjects;

public record JobCategoryResult {
    public required int JobCategoryId { get; set; }
    public required string Name { get; set; }
}
