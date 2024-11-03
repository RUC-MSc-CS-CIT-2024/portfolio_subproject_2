using Microsoft.EntityFrameworkCore;

namespace CitMovie.Data;
public class BookmarkRepository : IBookmarkRepository
{
    private readonly FrameworkContext _context;

    public BookmarkRepository(FrameworkContext context) =>
        _context = context;

    public async Task<Bookmark> AddBookmarkAsync(Bookmark bookmark)
    {
        _context.Bookmarks.Add(bookmark);
        await _context.SaveChangesAsync();
        return bookmark;
    }

    public async Task<Bookmark> GetBookmarkByIdAsync(int bookmarkId) =>
        await _context.Bookmarks
            .AsNoTracking()
            .Where(b => b.BookmarkId == bookmarkId)
            .FirstOrDefaultAsync();

    public async Task<IEnumerable<Bookmark>> GetUserBookmarksAsync(int userId) =>
        await _context.Bookmarks
            .AsNoTracking()
            .Where(b => b.UserId == userId)
            .ToListAsync();

    public async Task<Bookmark> UpdateBookmarkAsync(Bookmark bookmark)
    {
        var existingBookmark = await _context.Bookmarks
            .Where(b => b.BookmarkId == bookmark.BookmarkId)
            .FirstOrDefaultAsync();

        if (existingBookmark != null && existingBookmark.Note != bookmark.Note)
        {
            _context.Entry(existingBookmark).CurrentValues.SetValues(bookmark);
            await _context.SaveChangesAsync();
        }
        return existingBookmark;
    }

    public async Task<bool> DeleteBookmarkAsync(int bookmarkId)
    {
        try
        {
            var bookmark = await _context.Bookmarks
                .Where(b => b.BookmarkId == bookmarkId)
                .FirstOrDefaultAsync();

            if (bookmark == null) return false;

            _context.Bookmarks.Remove(bookmark);
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }
}