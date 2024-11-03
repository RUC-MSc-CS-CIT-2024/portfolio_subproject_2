using CitMovie.Business.Managers;
using CitMovie.Data.Repositories;
using CitMovie.Models.DomainObjects;
using CitMovie.Models.DTOs;
using FakeItEasy;
using System.Threading.Tasks;

public class BookmarkManagerTests
{
    private readonly IBookmarkRepository _bookmarkRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMediaRepository _mediaRepository;
    private readonly BookmarkManager _bookmarkManager;

    public BookmarkManagerTests()
    {
        _bookmarkRepository = A.Fake<IBookmarkRepository>();
        _userRepository = A.Fake<IUserRepository>();
        _mediaRepository = A.Fake<IMediaRepository>();
        _bookmarkManager = new BookmarkManager(_bookmarkRepository, _userRepository, _mediaRepository);
    }

   [Fact]
    public async Task CreateBookmark_ShouldReturnCreated_WhenValidDataIsProvided()
    {
        // Arrange
        var createBookmarkDto = new CreateBookmarkDto
        {
            MediaId = 101,
            MediaTitle = "Test Movie", // Media title provided by frontend
            Note = "Interesting movie!"
        };
        var createdBookmark = new BookmarkDto
        {
            BookmarkId = 1,
            UserId = 1,
            MediaId = 101,
            Note = "Structured Note Content"
        };

        A.CallTo(() => _bookmarkManager.CreateBookmarkAsync(A<CreateBookmarkDto>.Ignored)).Returns(createdBookmark);

        // Act
        var result = await _controller.CreateBookmark(createBookmarkDto);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        var returnedBookmark = Assert.IsType<BookmarkDto>(createdResult.Value);
        Assert.Equal(createdBookmark.BookmarkId, returnedBookmark.BookmarkId);
        Assert.Equal("Structured Note Content", returnedBookmark.Note);
        Assert.Equal(101, returnedBookmark.MediaId);
    }


    [Fact]
    public async Task GetBookmarkAsync_ShouldReturnNull_WhenBookmarkDoesNotExist()
    {
        // Arrange
        int nonExistentId = 99;
        A.CallTo(() => _bookmarkRepository.GetBookmarkByIdAsync(nonExistentId)).Returns((Bookmark)null);

        // Act
        var result = await _bookmarkManager.GetBookmarkAsync(nonExistentId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task UpdateBookmarkAsync_ShouldUpdateNote_WhenDifferentNoteProvided()
    {
        // Arrange
        var existingBookmark = new Bookmark { BookmarkId = 1, UserId = 1, MediaId = 101, Note = "Old note" };
        var newNote = "Updated note";

        A.CallTo(() => _bookmarkRepository.GetBookmarkByIdAsync(existingBookmark.BookmarkId)).Returns(existingBookmark);
        A.CallTo(() => _bookmarkRepository.UpdateBookmarkAsync(A<Bookmark>.Ignored)).Returns(existingBookmark);

        // Act
        var result = await _bookmarkManager.UpdateBookmarkAsync(existingBookmark.BookmarkId, newNote);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(newNote, result.Note);
    }

    [Fact]
    public async Task DeleteBookmarkAsync_ShouldReturnTrue_WhenBookmarkDeleted()
    {
        // Arrange
        int bookmarkId = 1;
        A.CallTo(() => _bookmarkRepository.DeleteBookmarkAsync(bookmarkId)).Returns(true);

        // Act
        var result = await _bookmarkManager.DeleteBookmarkAsync(bookmarkId);

        // Assert
        Assert.True(result);
    }
}
