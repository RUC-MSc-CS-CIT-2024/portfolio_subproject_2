using CitMovie.Api.Controllers;
using CitMovie.Business.Managers;
using CitMovie.Models.DataTransferObjects;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

public class BookmarkControllerTests
{
    private readonly IBookmarkManager _bookmarkManager;
    private readonly BookmarkController _controller;

    public BookmarkControllerTests()
    {
        _bookmarkManager = A.Fake<IBookmarkManager>();
        _controller = new BookmarkController(_bookmarkManager)
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim("user_id", "1") })) }
            }
        };
    }

    [Fact]
    public async Task GetBookmark_ShouldReturnUnauthorized_WhenAccessingOtherUsersBookmark()
    {
        // Arrange
        var otherUserClaim = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim("user_id", "2") }));
        _controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext { User = otherUserClaim } };

        // Act
        var result = await _controller.GetBookmark(1);

        // Assert
        Assert.IsType<UnauthorizedResult>(result);
    }

    [Fact]
    public async Task UpdateBookmark_ShouldReturnUnauthorized_WhenAccessingOtherUsersBookmark()
    {
        // Arrange
        var otherUserClaim = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim("user_id", "2") }));
        _controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext { User = otherUserClaim } };

        // Setting up a bookmark owned by user ID 1
        var bookmarkDto = new BookmarkDto { BookmarkId = 1, UserId = 1, MediaId = 101, Note = "Original Note" };
        A.CallTo(() => _bookmarkManager.GetBookmarkAsync(1)).Returns(bookmarkDto);

        // Act
        var result = await _controller.UpdateBookmark(1, "Updated Note");

        // Assert
        Assert.IsType<UnauthorizedResult>(result);
    }


    [Fact]
    public async Task CreateBookmark_ShouldReturnBadRequest_WhenNoteIsTooLong()
    {
        // Arrange
        var longNote = new string('a', 5001); // Exceeds typical length constraints
        var createBookmarkDto = new CreateBookmarkDto { MediaId = 101, Note = longNote };

        // Act
        var result = await _controller.CreateBookmark(createBookmarkDto);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task CreateBookmark_ShouldReturnCreatedResult_WhenSuccessful()
    {
        // Arrange
        var createDto = new CreateBookmarkDto { MediaId = 101, Note = "A great movie!" };
        var createdBookmark = new BookmarkDto { BookmarkId = 1, UserId = 1, MediaId = 101, Note = "Structured note content" };

        A.CallTo(() => _bookmarkManager.CreateBookmarkAsync(A<CreateBookmarkDto>.Ignored)).Returns(createdBookmark);

        // Act
        var result = await _controller.CreateBookmark(createDto);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal("GetBookmark", createdResult.ActionName);
        Assert.Equal(createdBookmark, createdResult.Value);
    }

    [Fact]
    public async Task CreateBookmark_ShouldReturnCreated_WhenValidDataIsProvided()
    {
        // Arrange
        var createBookmarkDto = new CreateBookmarkDto { MediaId = 101, Note = "Interesting movie!" };
        var createdBookmark = new BookmarkDto { BookmarkId = 1, UserId = 1, MediaId = 101, Note = "Structured Note Content" };

        // Mocking a successful creation
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
    public async Task GetBookmark_ShouldReturnNotFound_WhenBookmarkDoesNotExist()
    {
        // Arrange
        A.CallTo(() => _bookmarkManager.GetBookmarkAsync(99)).Returns((BookmarkDto)null);

        // Act
        var result = await _controller.GetBookmark(99);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task UpdateBookmark_ShouldReturnOkResult_WhenUpdateIsSuccessful()
    {
        // Arrange
        var updatedBookmark = new BookmarkDto { BookmarkId = 1, UserId = 1, MediaId = 101, Note = "Updated note" };

        A.CallTo(() => _bookmarkManager.UpdateBookmarkAsync(1, "Updated note")).Returns(updatedBookmark);

        // Act
        var result = await _controller.UpdateBookmark(1, "Updated note");

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(updatedBookmark, okResult.Value);
    }

    [Fact]
    public async Task DeleteBookmark_ShouldReturnNoContent_WhenSuccessful()
    {
        // Arrange
        A.CallTo(() => _bookmarkManager.DeleteBookmarkAsync(1)).Returns(true);

        // Act
        var result = await _controller.DeleteBookmark(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteBookmark_ShouldReturnNotFound_WhenBookmarkDoesNotExist()
    {
        // Arrange
        int nonExistentBookmarkId = 99;
        A.CallTo(() => _bookmarkManager.DeleteBookmarkAsync(nonExistentBookmarkId)).Returns(false);

        // Act
        var result = await _controller.DeleteBookmark(nonExistentBookmarkId);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

}
