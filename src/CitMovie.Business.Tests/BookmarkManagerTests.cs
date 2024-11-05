using CitMovie.Business;
using CitMovie.Data;
using CitMovie.Models.DomainObjects;
using CitMovie.Models.DataTransferObjects;
using FakeItEasy;
using System.Threading.Tasks;

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
    public async Task CreateBookmarkAsync_ShouldReturnCreated_WhenValidDataIsProvided()
    {
        // Arrange
        var createBookmarkDto = new CreateBookmarkDto { MediaId = 101, Note = "Interesting movie!" };
        var createdBookmark = new BookmarkDto { BookmarkId = 1, UserId = 1, MediaId = 101, Note = "Interesting movie!" };

        A.CallTo(() => _bookmarkRepository.BookmarkMediaAsync(createBookmarkDto.UserId, createBookmarkDto.MediaId, createBookmarkDto.Note)).Returns(Task.CompletedTask);
        A.CallTo(() => _bookmarkRepository.GetBookmarkByIdAsync(1)).Returns(createdBookmark);

        // Act
        var result = await _bookmarkManager.CreateBookmarkAsync(createBookmarkDto);

        // Assert
        Assert.Equal(createdBookmark, result);
    }

    [Fact]
    public async Task GetBookmarkAsync_ShouldReturnNull_WhenBookmarkDoesNotExist()
    {
        // Arrange
        int nonExistentId = 99;
        A.CallTo(() => _bookmarkRepository.GetBookmarkByIdAsync(nonExistentId)).Returns((BookmarkDto)null);

        // Act
        var result = await _bookmarkManager.GetBookmarkAsync(nonExistentId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task UpdateBookmarkAsync_ShouldUpdateNote_WhenDifferentNoteProvided()
    {
        // Arrange
        var existingBookmark = new BookmarkDto { BookmarkId = 1, UserId = 1, MediaId = 101, Note = "Old note" };
        var newNote = "Updated note";

        A.CallTo(() => _bookmarkRepository.GetBookmarkByIdAsync(existingBookmark.BookmarkId)).Returns(existingBookmark);
        A.CallTo(() => _bookmarkRepository.UpdateBookmarkAsync(existingBookmark.BookmarkId, newNote)).Returns(new BookmarkDto { BookmarkId = 1, UserId = 1, MediaId = 101, Note = newNote });

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
