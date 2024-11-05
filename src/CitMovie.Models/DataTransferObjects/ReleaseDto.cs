namespace CitMovie.Models.DataTransferObjects;

public class ReleaseResult
{
    public int ReleaseId { get; set; }
    public int MediaId { get; set; }
    public string? Rated { get; set; }
    public required string Type { get; set; }
    public IEnumerable<string>? SpokenLanguages { get; set; }
    public string? Country { get; set; }
    public string? Title { get; set; }
    public required DateTime ReleaseDate { get; set; }
    public List<Link> Links { get; set; } = new List<Link>();
}

public class ReleaseCreateRequest
{
    public required DateTime ReleaseDate { get; set; }
    public string? Rated { get; set; }
    public required string Type { get; set; }
    public IEnumerable<int>? SpokenLanguages { get; set; }
    public int? CountryId { get; set; }
}

public class ReleaseUpdateRequest
{
    public int? MediaId { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public string? Rated { get; set; }
    public string? Type { get; set; }
    public IEnumerable<int>? SpokenLanguages { get; set; }
    public int? CountryId { get; set; }
    public int? TitleId { get; set; }
}