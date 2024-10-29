using CitMovie.Models.DomainObjects;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CitMovie.Data.Repositories
{
    public class BookmarkRepository : IBookmarkRepository
    {
        private readonly FrameworkContext _context;

        public BookmarkRepository(FrameworkContext context)
        {
            _context = context;
        }

        public async Task<Bookmark> AddBookmarkAsync(Bookmark bookmark)
        {
            _context.Bookmarks.Add(bookmark);
            await _context.SaveChangesAsync();
            return bookmark;
        }

        public async Task<Bookmark> GetBookmarkByIdAsync(int bookmarkId)
        {
            return await _context.Bookmarks.FindAsync(bookmarkId);
        }

        public async Task<IEnumerable<Bookmark>> GetUserBookmarksAsync(int userId)
        {
            return await _context.Bookmarks.Where(b => b.UserId == userId).ToListAsync();
        }

        public async Task<Bookmark> UpdateBookmarkAsync(Bookmark bookmark)
        {
            _context.Bookmarks.Update(bookmark);
            await _context.SaveChangesAsync();
            return bookmark;
        }

        public async Task<bool> DeleteBookmarkAsync(int bookmarkId)
        {
            var bookmark = await _context.Bookmarks.FindAsync(bookmarkId);
            if (bookmark == null) return false;

            _context.Bookmarks.Remove(bookmark);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
