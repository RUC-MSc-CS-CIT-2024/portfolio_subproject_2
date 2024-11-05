namespace CitMovie.Models.DataTransferObjects;

public class ReleaseResult : BaseResult
{
    public int ReleaseId { get; set; }
    public int MediaId { get; set; }
    public string? Rated { get; set; }
    public required string Type { get; set; }
    public IEnumerable<string>? SpokenLanguages { get; set; }
    public string? Country { get; set; }
    public string? Title { get; set; }
    public required DateTime ReleaseDate { get; set; }
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
