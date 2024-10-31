namespace CitMovie.Models.DataTransferObjects;

public record PersonResult(int Id, string Name, string? Description, decimal? Score, DateTime? BirthDate, DateTime? DeathDate);
public record MediaResult(int MediaId, string Title, string Genre, string? Description);
public record CoActor(string CoActorName, string CoActorImdbId, int Frequency);

