namespace CitMovie.Models.DataTransferObjects;

public record MediaBasicResult(
    int Id,
    string Type,
    string Title,
    DateTime ReleaseDate,
    string PostUri);

public record MediaResult(
    int Id,
    string Type,
    string Title,
    DateTime ReleaseDate,
    string Rated,
    string PostUri,
    string Plot,
    int RuntimeMinutes,
    int BoxOffice,
    int Budget,
    IEnumerable<GenreResult> Genres,
    IEnumerable<ScoreResult> Scores,
    IEnumerable<MediaProductionCompanyResult> ProductionCompanies,
    IEnumerable<CountryResult> ProductionCountries);

public record ScoreResult(string Source, string Value, int VoteCount);
public record MediaProductionCompanyResult(int Id, string Name);
