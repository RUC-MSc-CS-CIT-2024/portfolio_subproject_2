namespace CitMovie.Business;

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
        // Call the database function to add a bookmark
        await _bookmarkRepository.BookmarkMediaAsync(createBookmarkDto.UserId, createBookmarkDto.MediaId, createBookmarkDto.Note);

        // Return a DTO representing the newly created bookmark
        return new BookmarkDto
        {
            UserId = createBookmarkDto.UserId,
            MediaId = createBookmarkDto.MediaId,
            Note = createBookmarkDto.Note
        };
    }

    public async Task<BookmarkDto> UpdateBookmarkAsync(int bookmarkId, string? note)
    {
        var existingBookmark = await _bookmarkRepository.GetBookmarkByIdAsync(bookmarkId);
        if (existingBookmark == null) return null;

        // Calls the updated repository method, which uses EF to update the note
        existingBookmark = await _bookmarkRepository.UpdateBookmarkAsync(bookmarkId, note);

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

    public async Task<IEnumerable<BookmarkDto>> GetUserBookmarksAsync(int userId, int page, int pageSize) =>
        (await _bookmarkRepository.GetUserBookmarksAsync(userId, page, pageSize))
            .Select(b => new BookmarkDto
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

    public async Task<int> GetTotalUserBookmarksCountAsync(int userId)
    {
        return await _bookmarkRepository.GetTotalUserBookmarksCountAsync(userId);
    }
}
