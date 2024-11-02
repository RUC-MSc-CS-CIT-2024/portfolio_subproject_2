namespace CitMovie.Models.DataTransferObjects;

public record PersonResult(int Id, string Name, string? Description, decimal? Score, decimal? NameRating, DateTime? BirthDate, DateTime? DeathDate)
{
    public List<Link> Links { get; set; } = new List<Link>();
}
public record MediaResult(int MediaId, string Title, string? Type, IEnumerable<string> Genres, string? Description, string? Awards, int BoxOffice)
{
    public List<Link> Links { get; set; } = new List<Link>();
}
public record CoActorResult(string Id, string ActorName, int Frequency)
{
    public List<Link> Links { get; set; } = new List<Link>();
}
