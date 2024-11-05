using CitMovie.Api;
using CitMovie.Business;
using CitMovie.Models.DataTransferObjects;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim("user_id", "1") }))
                }
            }
        };
    }

    [Fact]
    public async Task CreateBookmark_ShouldReturnCreatedResult_WhenValidDataIsProvided()
    {
        var createDto = new CreateBookmarkDto { MediaId = 101, Note = "Great movie!" };
        var createdBookmark = new BookmarkDto { BookmarkId = 1, UserId = 1, MediaId = 101, Note = "Great movie!" };

        _ = A.CallTo(() => _bookmarkManager.CreateBookmarkAsync(A<CreateBookmarkDto>.Ignored)).Returns(createdBookmark);

        var result = await _controller.CreateBookmark(1, createDto);

        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal("GetBookmark", createdResult.ActionName);
        Assert.Equal(createdBookmark, createdResult.Value);
    }

    [Fact]
    public async Task GetBookmark_ShouldReturnForbid_WhenAccessingOtherUsersBookmark()
    {
        var otherUserClaim = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim("user_id", "2") }));
        _controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext { User = otherUserClaim } };

        var result = await _controller.GetBookmark(1, 1);

        _ = Assert.IsType<ForbidResult>(result);
    }

    [Fact]
    public async Task UpdateBookmark_ShouldReturnNotFound_WhenBookmarkDoesNotExist()
    {
        _ = A.CallTo(() => _bookmarkManager.GetBookmarkAsync(99)).Returns(Task.FromResult<BookmarkDto>(null!));

        var result = await _controller.UpdateBookmark(1, 99, "Updated note");

        _ = Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task DeleteBookmark_ShouldReturnNoContent_WhenSuccessful()
    {
        _ = A.CallTo(() => _bookmarkManager.DeleteBookmarkAsync(1)).Returns(true);

        var result = await _controller.DeleteBookmark(1, 1);

        _ = Assert.IsType<NoContentResult>(result);
    }
}
