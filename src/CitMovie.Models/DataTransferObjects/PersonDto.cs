namespace CitMovie.Models.DataTransferObjects;

public record PersonResult(int Id, string Name, string? Description, decimal? Score, decimal? NameRating, DateTime? BirthDate, DateTime? DeathDate)
{
    public List<Link> Links { get; set; } = new List<Link>();
}
public record MediaResult(int MediaId, string Title, string? Type, string Genre, string? Description, string? Awards, int BoxOffice)
{
    public List<Link> Links { get; set; } = new List<Link>();
}
public record CoActorResult(string Id, string ActorName, int Frequency)
{
    public List<Link> Links { get; set; } = new List<Link>();
}


public class Link
{
    public string Href { get; set; }
    public string Rel { get; set; }
    public string Method { get; set; }
}