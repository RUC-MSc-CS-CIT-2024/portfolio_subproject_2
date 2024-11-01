namespace CitMovie.Models.DataTransferObjects;

public class ReleaseResult
{
    public int ReleaseId { get; set; }
    public string Rated { get; set; }
    public string Type { get; set; }
    public IEnumerable<string> SpokenLanguages { get; set; }
    public string? Country { get; set; }
    public string? Title { get; set; }
    
}

public class ReleaseCreateRequest
{
    public string Rated { get; set; }
    public string Type { get; set; }
    public IEnumerable<int> SpokenLanguages { get; set; }
    public int CountryId { get; set; }
}

public class ReleaseUpdateRequest
{
    public string? Rated { get; set; }
    public string? Type { get; set; }
    public IEnumerable<string>? SpokenLanguages { get; set; }
    public string? Country { get; set; }
    public string? Title { get; set; }
}