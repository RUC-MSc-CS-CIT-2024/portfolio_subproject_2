using System.ComponentModel;

namespace CitMovie.Models.DataTransferObjects;

public class MediaBasicResult : BaseResult {
    public required int Id { get; set; }
    public required string Type { get; set; }
    public required string Title { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public string? PosterUri { get; set; }
    public List<GenreNameResult> Genres { get; set; } = [];
    public List<CountryNameResult> ProductionCountries { get; set; } = [];
    public List<MediaProductionCompanyNameResult> ProductionCompanies { get; set; } = [];
    
    public class MediaProductionCompanyNameResult {
        public required string Name { get; set; }
    }
    
}

public class MediaResult : BaseResult {
    public required int Id { get; set; }
    public required string Type { get; set; }
    public required string Title { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public string? Rated { get; set; }
    public string? PosterUri { get; set; }
    public string? Plot { get; set; }
    public int? RuntimeMinutes { get; set; }
    public int? BoxOffice { get; set; }
    public int? Budget { get; set; }
    public string? AwardText { get; set; }
    public string? ImdbId { get; set; }
    public List<GenreResult> Genres { get; set; } = [];
    public List<ScoreResult> Scores { get; set; } = [];
    public List<MediaProductionCompanyResult> ProductionCompanies { get; set; } = [];
    public List<CountryResult> ProductionCountries { get; set; } = [];

    // Season info
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? SeasonNumber { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTime? EndDate { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Status { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? SeriesId { get; set; }

    // Episode info
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? EpisodeNumber { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? SeasonId { get; set; }

    public class ScoreResult {
        public required string Source { get; set; }
        public required string Value { get; set; }
        public int? VoteCount { get; set; }
    }

    public class MediaProductionCompanyResult {
        public required int Id { get; set; }
        public required string Name { get; set; }
    }
}


[JsonConverter(typeof(JsonStringEnumConverter<MediaQueryType>))]
public enum MediaQueryType {
    All,
    ExactMatch,
    BestMatch,
    Simple,
    Structured
}

public class MediaQueryParameter {
    public PageQueryParameter Page { get; init; } = new PageQueryParameter();

    [FromQuery(Name = "query_type"), DefaultValue(MediaQueryType.All)]
    public MediaQueryType QueryType { get; init; } = MediaQueryType.All;
    
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
    
    [FromQuery(Name = "type")]
    public string? Type { get; init; }
    
    [FromQuery(Name = "year")]
    public int? Year { get; init; }
    
    [FromQuery(Name = "genre")]
    public string? Genre { get; init; }
    
    [FromQuery(Name = "country")]
    public string? Country { get; init; }    
}
