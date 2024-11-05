using CitMovie.Api;
using CitMovie.Business;
using CitMovie.Models.DataTransferObjects;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

public class BookmarkControllerTests
{
    private readonly IBookmarkManager _bookmarkManager;
    private readonly IUserManager _userManager;
    private readonly IMediaManager _mediaManager;
    private readonly PagingHelper _pagingHelper;
    private readonly BookmarkController _controller;

    // Constants for the user details
    private const int TestUserId = 8;
    private const string TestUsername = "julius";

    public BookmarkControllerTests()
    {
        _bookmarkManager = A.Fake<IBookmarkManager>();
        _userManager = A.Fake<IUserManager>();
        _mediaManager = A.Fake<IMediaManager>();
        _pagingHelper = A.Fake<PagingHelper>();

        _controller = new BookmarkController(_bookmarkManager, _userManager, _mediaManager, _pagingHelper);
    }

    [Fact]
    public async Task GetBookmark_ShouldReturnNotFound_WhenBookmarkDoesNotExist()
    {
        // Arrange
        A.CallTo(() => _bookmarkManager.GetBookmarkAsync(99)).Returns((BookmarkDto)null);

        // Act
        var result = await _controller.GetBookmark(TestUserId, 99);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task GetBookmark_ShouldReturnBookmark_WhenExists()
    {
        // Arrange
        var bookmark = new BookmarkDto { BookmarkId = 1, UserId = 8, MediaId = 101, Note = "Test note" };
        A.CallTo(() => _bookmarkManager.GetBookmarkAsync(1)).Returns(bookmark);

        // Act
        var result = await _controller.GetBookmark(8, 1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(bookmark, okResult.Value);
    }
}
