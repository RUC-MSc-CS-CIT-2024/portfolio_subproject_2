using CitMovie.Business;
using CitMovie.Data;
using CitMovie.Models.DataTransferObjects;
using CitMovie.Models.DomainObjects;
using FakeItEasy;

public class BookmarkManagerTests
{
    private readonly IBookmarkRepository _bookmarkRepository;
    private readonly IUserRepository _userRepository;
    private readonly BookmarkManager _bookmarkManager;

    public BookmarkManagerTests()
    {
        _bookmarkRepository = A.Fake<IBookmarkRepository>();
        _userRepository = A.Fake<IUserRepository>();
        _bookmarkManager = new BookmarkManager(_bookmarkRepository, _userRepository);
    }

    [Fact]
    public async Task CreateBookmarkAsync_ShouldReturnCreatedBookmark_WhenValidDataIsProvided()
    {
        var createBookmarkDto = new CreateBookmarkDto
        {
            UserId = 1,
            MediaId = 101,
            Note = "Interesting movie!"
        };
        var createdBookmark = new Bookmark
        {
            BookmarkId = 1,
            UserId = 1,
            MediaId = 101,
            Note = "Interesting movie!"
        };

        A.CallTo(() => _bookmarkRepository.AddBookmarkAsync(A<Bookmark>.Ignored)).Returns(createdBookmark);

        var result = await _bookmarkManager.CreateBookmarkAsync(createBookmarkDto);

        Assert.NotNull(result);
        Assert.Equal(1, result.BookmarkId);
        Assert.Equal("Interesting movie!", result.Note);
    }

    [Fact]
    public async Task UpdateBookmarkAsync_ShouldUpdateNote_WhenNoteIsProvided()
    {
        var existingBookmark = new Bookmark { BookmarkId = 1, UserId = 1, MediaId = 101, Note = "Old note" };
        var updatedBookmark = new Bookmark { BookmarkId = 1, UserId = 1, MediaId = 101, Note = "Updated note" };

        A.CallTo(() => _bookmarkRepository.GetBookmarkByIdAsync(1)).Returns(existingBookmark);
        A.CallTo(() => _bookmarkRepository.UpdateBookmarkAsync(A<Bookmark>.Ignored)).Returns(updatedBookmark);

        var result = await _bookmarkManager.UpdateBookmarkAsync(1, "Updated note");

        Assert.Equal("Updated note", result.Note);
    }

    [Fact]
    public async Task DeleteBookmarkAsync_ShouldReturnTrue_WhenBookmarkIsDeleted()
    {
        A.CallTo(() => _bookmarkRepository.DeleteBookmarkAsync(1)).Returns(true);

        var result = await _bookmarkManager.DeleteBookmarkAsync(1);

        Assert.True(result);
    }
}
