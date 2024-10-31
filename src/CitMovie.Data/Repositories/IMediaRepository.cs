namespace CitMovie.Data;

public interface IMediaRepository {
    IEnumerable<Media> SearchBestMatch(string[] keywords, int page, int pageSize);
    IEnumerable<Media> SearchExactMatch(string[] keywords, int page, int pageSize);
    IEnumerable<Media> SearchSimple(string query, int userId, int page, int pageSize);
    IEnumerable<Media> SearchStructured(string title, string plot, string character, string person, int userId, int page, int pageSize);
    IEnumerable<Media> GetAllMedia(int page, int pageSize);
    IEnumerable<Media> GetAllMedia(Func<Media, bool> wherePredicate, int page, int pageSize);
    IEnumerable<Media> GetSimilarMedia(int id, int page, int pageSize);
    Media? GetMediaById(int id);
}