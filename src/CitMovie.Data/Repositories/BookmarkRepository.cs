using Npgsql;

namespace CitMovie.Data;

public class BookmarkRepository : IBookmarkRepository
{
    private readonly FrameworkContext _context;

    public BookmarkRepository(FrameworkContext context) =>
        _context = context;

    public async Task<Bookmark> BookmarkMediaAsync(int userId, int mediaId, string? note = null)
    {
        var sql = "SELECT bookmark_media(@p_user_id, @p_media_id, @p_note)";
        var parameters = new[]
        {
            new NpgsqlParameter("p_user_id", userId),
            new NpgsqlParameter("p_media_id", mediaId),
            new NpgsqlParameter("p_note", note ?? (object)DBNull.Value)
        };

        await _context.Database.ExecuteSqlRawAsync(sql, parameters);

        return await _context.Bookmarks.FirstAsync(x => x.UserId == userId && x.MediaId == mediaId);
    }

    public async Task UnbookmarkMediaAsync(int userId, int mediaId)
    {
        var sql = "SELECT unbookmark_media(@p_user_id, @p_media_id)";
        var parameters = new[]
        {
            new NpgsqlParameter("p_user_id", userId),
            new NpgsqlParameter("p_media_id", mediaId)
        };

        await _context.Database.ExecuteSqlRawAsync(sql, parameters);
    }

    public async Task<Bookmark> UpdateBookmarkAsync(int bookmarkId, string? note)
{
    var bookmark = await _context.Bookmarks.FindAsync(bookmarkId);
    if (bookmark == null)
    {
        throw new KeyNotFoundException($"Bookmark with ID {bookmarkId} was not found.");
    }
    bookmark.Note = note;
    await _context.SaveChangesAsync();

    return bookmark;
}

    public async Task<Bookmark> GetBookmarkByIdAsync(int bookmarkId) =>
        await _context.Bookmarks
            .AsNoTracking()
            .Where(b => b.BookmarkId == bookmarkId)
            .FirstAsync();

    public async Task<IEnumerable<Bookmark>> GetUserBookmarksAsync(int userId, int page, int pageSize) =>
        await _context.Bookmarks
            .AsNoTracking()
            .Where(b => b.UserId == userId)
            .Pagination(page, pageSize)
            .ToListAsync();

    public async Task<int> GetTotalUserBookmarksCountAsync(int userId) =>
        await _context.Bookmarks.CountAsync(b => b.UserId == userId);
}
