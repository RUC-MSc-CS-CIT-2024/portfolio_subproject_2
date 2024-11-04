using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CitMovie.Data;

public interface IMediaRepository {
    IEnumerable<Media> SearchBestMatch(string[] keywords, int page, int pageSize);
    IEnumerable<Media> SearchExactMatch(string[] keywords, int page, int pageSize);
    IEnumerable<Media> SearchSimple(string query, int userId, int page, int pageSize);
    IEnumerable<Media> SearchStructured(string? title, string? plot, string? character, string? person, int userId, int page, int pageSize);
    IEnumerable<Media> GetAll(int page, int pageSize);
    IEnumerable<Media> GetAll(Func<Media, bool> wherePredicate, int page, int pageSize);
    IEnumerable<Media> GetSimilar(int id, int page, int pageSize);
    IEnumerable<Media> GetRelated(int id, int page, int pageSize);
    Media? GetDetailed(int id);
    Media? Get(int id);
}