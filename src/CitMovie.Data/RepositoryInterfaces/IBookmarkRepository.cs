namespace CitMovie.Data;
public interface IBookmarkRepository
{
    Task<Bookmark> BookmarkMediaAsync(int userId, int mediaId, string? note = null);
    Task UnbookmarkMediaAsync(int userId, int mediaId);
    Task<Bookmark> UpdateBookmarkAsync(int bookmarkId, string? note);
    Task<Bookmark?> GetBookmarkByIdAsync(int bookmarkId);
    Task<IEnumerable<Bookmark>> GetUserBookmarksAsync(int userId, int page, int pageSize);
    Task<int> GetTotalUserBookmarksCountAsync(int userId);
}
