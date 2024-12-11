namespace CitMovie.Models.DataTransferObjects;

public class PersonResult : BaseResult
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public decimal? Score { get; set; }
    public decimal? NameRating { get; set; }
    public DateTime? BirthDate { get; set; }
    public DateTime? DeathDate { get; set; }
    public string? ImdbId { get; set; }
}

public class CoActorResult : BaseResult
{
    public required int Id { get; set; }
    public required string ActorName { get; set; }
    public required int Frequency { get; set; }
}

public class PersonCrewResult : BaseResult {
    public required int Id { get; set; }
    public required string Role { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Character { get; set; }
    public required string JobCategory { get; set; }
    public MediaResult? Media { get; set; }

    public class MediaResult : BaseResult {
        public required int Id { get; set; }
        public required string Type { get; set; }
        public required string Title { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string? PosterUri { get; set; }
    }
}

public class PersonQueryParameter {
    [FromQuery(Name = "name")]
    public string? Name { get; init; }
}
