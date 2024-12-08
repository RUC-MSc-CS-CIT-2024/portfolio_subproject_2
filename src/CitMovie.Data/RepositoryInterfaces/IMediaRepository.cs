using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CitMovie.Data;

public interface IMediaRepository
{
    IEnumerable<Media> SearchBestMatch(string[] keywords, int page, int pageSize, FilterParameters? parameter);
    IEnumerable<Media> SearchExactMatch(string[] keywords, int page, int pageSize, FilterParameters? parameter);
    IEnumerable<Media> SearchSimple(string query, int userId, int page, int pageSize, FilterParameters? parameter);
    IEnumerable<Media> SearchStructured(string? title, string? plot, string? character, string? person, int userId,
        int page, int pageSize, FilterParameters? parameter);
    IEnumerable<Media> GetAll(int page, int pageSize, FilterParameters? parameters);
    IEnumerable<Media> GetSimilar(int id, int page, int pageSize);
    IEnumerable<Media> GetRelated(int id, int page, int pageSize);
    Media? GetDetailed(int id);
    Media? Get(int id);
    Task<int> GetTotalRelatedMediaCountAsync(int id);
    Task<int> GetTotalSimilarMediaCountAsync(int id);
    int GetSimpleSearchResultsCount(string query, FilterParameters? parameter);
    int GetBestMatchSearchResultsCount(string[] keywords, FilterParameters? parameter);
    int GetExactMatchSearchResultsCount(string[] keywords, FilterParameters? parameter);
    int GetStructuredSearchResultsCount(string? title, string? plot, string? character, string? person, FilterParameters? parameter);
    int GetTotalMediaCount(FilterParameters? parameters);
}