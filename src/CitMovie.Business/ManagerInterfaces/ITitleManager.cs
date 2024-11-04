namespace CitMovie.Business;

public interface ITitleManager {
    IEnumerable<TitleResult> GetAllForMedia(int mediaId, PageQueryParameter page);
    TitleResult Get(int mediaId, int titleId);
    Task<TitleResult> CreateAsync(int mediaId, TitleCreateRequest createRequest);
    Task<bool> DeleteAsync(int mediaId, int titleId);
}