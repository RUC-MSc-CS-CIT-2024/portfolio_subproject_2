using System.Text.Json.Serialization;

namespace CitMovie.Models.DataTransferObjects;

public class MediaBasicResult {
    public required int Id { get; set; }
    public required string Type { get; set; }
    public required string Title { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public string? PosterUri { get; set; }
}

public record MediaResult(
    int Id,
    string Type,
    string Title,
    DateTime? ReleaseDate,
    string? Rated,
    string? PosterUri,
    string? Plot,
    int? RuntimeMinutes,
    int? BoxOffice,
    int? Budget,
    IEnumerable<GenreResult> Genres,
    IEnumerable<ScoreResult> Scores,
    IEnumerable<MediaProductionCompanyResult> ProductionCompanies,
    IEnumerable<CountryResult> ProductionCountries);

public record ScoreResult(string Source, string Value, int VoteCount);
public record MediaProductionCompanyResult(int Id, string Name);

[JsonConverter(typeof(JsonStringEnumConverter<MediaQueryType>))]
public enum MediaQueryType {
    Basic,
    ExactMatch,
    BestMatch,
    Simple,
    Structured
}

public class MediaQueryParameter {
    public PageQueryParameter Page { get; init; } = new PageQueryParameter();

    [FromQuery(Name = "query_type")]
    public MediaQueryType QueryType { get; init; } = MediaQueryType.Basic;
    
    [FromQuery(Name = "keywords")]
    public string[]? Keywords { get; init; }

    [FromQuery(Name = "query")]
    public string? Query { get; init; }

    [FromQuery(Name = "title")]
    public string? Title { get; init; }

    [FromQuery(Name = "plot")]
    public string? Plot { get; init; }

    [FromQuery(Name = "character")]
    public string? Character { get; init; }

    [FromQuery(Name = "person")]
    public string? PersonName { get; init; }
}
