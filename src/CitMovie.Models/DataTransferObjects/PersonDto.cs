namespace CitMovie.Models.DataTransferObjects;

public record PersonResult
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal? Score { get; set; }
    public decimal? NameRating { get; set; }
    public DateTime? BirthDate { get; set; }
    public DateTime? DeathDate { get; set; }
    public List<Link> Links { get; set; } = new List<Link>();
}

public record MediaResult
{
    public int MediaId { get; set; }
    public string Title { get; set; }
    public string? Type { get; set; }
    public IEnumerable<string> Genres { get; set; }
    public string? Description { get; set; }
    public string? Awards { get; set; }
    public int BoxOffice { get; set; }
    public List<Link> Links { get; set; } = new List<Link>();
}

public record CoActorResult
{
    public string Id { get; set; }
    public string ActorName { get; set; }
    public int Frequency { get; set; }
    public List<Link> Links { get; set; } = new List<Link>();
}