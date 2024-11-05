namespace CitMovie.Business;

public class BookmarkManager : IBookmarkManager
{
    private readonly IBookmarkRepository _bookmarkRepository;
    private readonly IMapper _mapper;

    public BookmarkManager(IBookmarkRepository bookmarkRepository, IMapper mapper)
    {
        _bookmarkRepository = bookmarkRepository;
        _mapper = mapper;
    }

    public async Task<BookmarkResult> CreateBookmarkAsync(int userId, BookmarkCreateRequest createBookmarkDto)
    {
        // Call the database function to add a bookmark
        Bookmark result = await _bookmarkRepository.BookmarkMediaAsync(userId, createBookmarkDto.MediaId, createBookmarkDto.Note);
        return _mapper.Map<BookmarkResult>(result);
    }

    public async Task<BookmarkResult> UpdateBookmarkAsync(int bookmarkId, string? note)
    {
        var existingBookmark = await _bookmarkRepository.GetBookmarkByIdAsync(bookmarkId);
        if (existingBookmark == null) return null;

        // Calls the updated repository method, which uses EF to update the note
        existingBookmark = await _bookmarkRepository.UpdateBookmarkAsync(bookmarkId, note);

        return new BookmarkResult
        {
            BookmarkId = existingBookmark.BookmarkId,
            UserId = existingBookmark.UserId,
            MediaId = existingBookmark.MediaId,
            Note = existingBookmark.Note
        };
    }

    public async Task<BookmarkResult> GetBookmarkAsync(int bookmarkId) =>
        await TransformToBookmarkDto(await _bookmarkRepository.GetBookmarkByIdAsync(bookmarkId));

    public async Task<IEnumerable<BookmarkResult>> GetUserBookmarksAsync(int userId, int page, int pageSize) =>
        (await _bookmarkRepository.GetUserBookmarksAsync(userId, page, pageSize))
            .Select(b => new BookmarkResult
            {
                BookmarkId = b.BookmarkId,
                UserId = b.UserId,
                MediaId = b.MediaId,
                Note = b.Note
            });

    public async Task<bool> DeleteBookmarkAsync(int bookmarkId)
    {
        var bookmark = await _bookmarkRepository.GetBookmarkByIdAsync(bookmarkId);
        if (bookmark == null) return false;

        // Call the database function to remove the bookmark
        await _bookmarkRepository.UnbookmarkMediaAsync(bookmark.UserId, bookmark.MediaId);
        return true;
    }

    private async Task<BookmarkResult> TransformToBookmarkDto(Bookmark bookmark)
    {
        return bookmark == null ? null : new BookmarkResult
        {
            BookmarkId = bookmark.BookmarkId,
            UserId = bookmark.UserId,
            MediaId = bookmark.MediaId,
            Note = bookmark.Note
        };
    }

    public async Task<int> GetTotalUserBookmarksCountAsync(int userId)
    {
        return await _bookmarkRepository.GetTotalUserBookmarksCountAsync(userId);
    }
}
