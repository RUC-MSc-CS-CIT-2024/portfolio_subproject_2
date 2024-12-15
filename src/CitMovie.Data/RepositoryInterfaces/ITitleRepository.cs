namespace CitMovie.Data;

public interface ITitleRepository {
    IEnumerable<Title> GetForMedia(int mediaId, int page, int count);
    Title Get(int titleId);
    Task<Title> CreateAsync(Title title);
    Task<bool> DeleteAsync(int  titleId);
    int GetTotalTitles(int mediaId);
}
