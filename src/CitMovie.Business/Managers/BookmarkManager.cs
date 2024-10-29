using CitMovie.Data.Repositories;
using CitMovie.Models.DomainObjects;
using CitMovie.Models.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitMovie.Business.Managers
{
    public class BookmarkManager : IBookmarkManager
    {
        private readonly IBookmarkRepository _bookmarkRepository;

        public BookmarkManager(IBookmarkRepository bookmarkRepository)
        {
            _bookmarkRepository = bookmarkRepository;
        }

        public async Task<BookmarkDto> CreateBookmarkAsync(CreateBookmarkDto createBookmarkDto)
        {
            var newBookmark = new Bookmark
            {
                UserId = createBookmarkDto.UserId,
                MediaId = createBookmarkDto.MediaId,
                Note = createBookmarkDto.Note
            };
            
            var result = await _bookmarkRepository.AddBookmarkAsync(newBookmark);

            return new BookmarkDto
            {
                BookmarkId = result.BookmarkId,
                UserId = result.UserId,
                MediaId = result.MediaId,
                Note = result.Note
            };
        }

        public async Task<BookmarkDto> GetBookmarkAsync(int bookmarkId)
        {
            var bookmark = await _bookmarkRepository.GetBookmarkByIdAsync(bookmarkId);
            return bookmark == null ? null : new BookmarkDto
            {
                BookmarkId = bookmark.BookmarkId,
                UserId = bookmark.UserId,
                MediaId = bookmark.MediaId,
                Note = bookmark.Note
            };
        }

        public async Task<IEnumerable<BookmarkDto>> GetUserBookmarksAsync(int userId)
        {
            var bookmarks = await _bookmarkRepository.GetUserBookmarksAsync(userId);
            return bookmarks.Select(b => new BookmarkDto
            {
                BookmarkId = b.BookmarkId,
                UserId = b.UserId,
                MediaId = b.MediaId,
                Note = b.Note
            });
        }

        public async Task<BookmarkDto> UpdateBookmarkAsync(int bookmarkId, string? note)
        {
            var existingBookmark = await _bookmarkRepository.GetBookmarkByIdAsync(bookmarkId);
            if (existingBookmark == null) return null;

            existingBookmark.Note = note;
            var updatedBookmark = await _bookmarkRepository.UpdateBookmarkAsync(existingBookmark);

            return new BookmarkDto
            {
                BookmarkId = updatedBookmark.BookmarkId,
                UserId = updatedBookmark.UserId,
                MediaId = updatedBookmark.MediaId,
                Note = updatedBookmark.Note
            };
        }

        public async Task<bool> DeleteBookmarkAsync(int bookmarkId)
            => await _bookmarkRepository.DeleteBookmarkAsync(bookmarkId);
    }
}
