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
        private readonly IUserRepository _userRepository;

        public BookmarkManager(IBookmarkRepository bookmarkRepository, IUserRepository userRepository)
        {
            _bookmarkRepository = bookmarkRepository;
            _userRepository = userRepository;
        }

        public async Task<BookmarkDto> CreateBookmarkAsync(CreateBookmarkDto createBookmarkDto)
        {
            var user = await _userRepository.GetUserAsync(createBookmarkDto.UserId);

            var newBookmark = new Bookmark
            {
                UserId = createBookmarkDto.UserId,
                MediaId = createBookmarkDto.MediaId,
                Note = createBookmarkDto.Note != null
                    ? new Note(user.Username, createBookmarkDto.MediaTitle, createBookmarkDto.Note).ToString()
                    : null
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

        public async Task<BookmarkDto> UpdateBookmarkAsync(int bookmarkId, string? note)
        {
            var existingBookmark = await _bookmarkRepository.GetBookmarkByIdAsync(bookmarkId);
            if (existingBookmark == null) return null;

            var user = await _userRepository.GetUserAsync(existingBookmark.UserId);

            if (existingBookmark.Note != note)
            {
                existingBookmark.Note = note != null
                    ? new Note(user.Username, existingBookmark.Note, note).ToString()
                    : null;
                existingBookmark = await _bookmarkRepository.UpdateBookmarkAsync(existingBookmark);
            }

            return new BookmarkDto
            {
                BookmarkId = existingBookmark.BookmarkId,
                UserId = existingBookmark.UserId,
                MediaId = existingBookmark.MediaId,
                Note = existingBookmark.Note
            };
        }

        public async Task<BookmarkDto> GetBookmarkAsync(int bookmarkId) =>
            await TransformToBookmarkDto(await _bookmarkRepository.GetBookmarkByIdAsync(bookmarkId));

        public async Task<IEnumerable<BookmarkDto>> GetUserBookmarksAsync(int userId) =>
            (await _bookmarkRepository.GetUserBookmarksAsync(userId))
                .Select(b => new BookmarkDto
                {
                    BookmarkId = b.BookmarkId,
                    UserId = b.UserId,
                    MediaId = b.MediaId,
                    Note = b.Note
                });

        public async Task<bool> DeleteBookmarkAsync(int bookmarkId) =>
            await _bookmarkRepository.DeleteBookmarkAsync(bookmarkId);

        private async Task<BookmarkDto> TransformToBookmarkDto(Bookmark bookmark)
        {
            return bookmark == null ? null : new BookmarkDto
            {
                BookmarkId = bookmark.BookmarkId,
                UserId = bookmark.UserId,
                MediaId = bookmark.MediaId,
                Note = bookmark.Note
            };
        }
    }
}
