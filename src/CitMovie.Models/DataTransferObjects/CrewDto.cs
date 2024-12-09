namespace CitMovie.Models.DataTransferObjects;

public class CrewResult : BaseResult {
    public required int Id { get; set; }
    public required string Role { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Character { get; set; }
    public required string JobCategory { get; set; }
    public required int PersonId { get; set; }
    public required string PersonName { get; set; }
}
