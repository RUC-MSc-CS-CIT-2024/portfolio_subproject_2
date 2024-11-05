namespace CitMovie.Data;

public interface ITitleRepository {
    IEnumerable<Title> GetForMedia(int mediaId);
    Title Get(int titleId);
    Task<Title> CreateAsync(Title title);
    Task<bool> DeleteAsync(int  titleId);
}
