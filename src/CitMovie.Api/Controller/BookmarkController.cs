using CitMovie.Api.Controllers;

namespace CitMovie.Api;

[ApiController]
[Route("api/users/{userId}/bookmarks")]
[Authorize(Policy = "user_scope")]
[Tags("User")]
public class BookmarkController : ControllerBase
{
    private readonly IBookmarkManager _bookmarkManager;
    private readonly ICompletedManager _completedManager;
    private readonly IUserManager _userManager;
    private readonly IMediaManager _mediaManager;
    private readonly PagingHelper _pagingHelper;
    private readonly ILogger<BookmarkController> _logger;

    public BookmarkController(
        IBookmarkManager bookmarkManager, 
        ICompletedManager completedManager,
        IUserManager userManager, 
        IMediaManager mediaManager,
        PagingHelper pagingHelper,
        ILogger<BookmarkController> logger)
    {
        _bookmarkManager = bookmarkManager;
        _completedManager = completedManager;
        _userManager = userManager;
        _mediaManager = mediaManager;
        _pagingHelper = pagingHelper;
        _logger = logger;
    }


    [HttpPost]
    public async Task<IActionResult> CreateBookmark(int userId, [FromBody] BookmarkCreateRequest createBookmarkDto)
    {
        if (createBookmarkDto == null)
            return BadRequest("Bookmark data is required.");

        var createdBookmark = await _bookmarkManager.CreateBookmarkAsync(userId, createBookmarkDto);
        createdBookmark.Links = AddBookmarkLinks(createdBookmark);
        return CreatedAtAction(nameof(GetBookmark), new { userId, id = createdBookmark.BookmarkId }, createdBookmark);
    }

    [HttpPost("{id}/move")]
    public async Task<IActionResult> MoveBookmarkToCompleted(int userId, int id, [FromBody] BookmarkMoveRequest bookmarkMoveRequest)
    {
        if (bookmarkMoveRequest == null)
            return BadRequest("Completed data is required.");

        try {
            var completedItem = await _completedManager.MoveBookmarkToCompletedAsync(userId, id, bookmarkMoveRequest);
            return Created($"/api/users/{userId}/completed/{completedItem.CompletedId}", completedItem);
        } catch (Exception e) {
            _logger.LogError(e, "Unexpected error: ");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("{id}", Name = nameof(GetBookmark))]
    public async Task<IActionResult> GetBookmark(int userId, int id)
    {
        var bookmark = await _bookmarkManager.GetBookmarkAsync(id);
        if (bookmark == null)
            return NotFound();

        if (bookmark.UserId != userId)
            return Forbid();

        bookmark.Links = AddBookmarkLinks(bookmark);

        return Ok(bookmark);
    }

    [HttpGet(Name = nameof(GetUserBookmarks))]
    public async Task<IActionResult> GetUserBookmarks(int userId, [FromQuery] PageQueryParameter page)
    {
        var bookmarks = await _bookmarkManager.GetUserBookmarksAsync(userId, page.Number, page.Count);
        var totalItems = await _bookmarkManager.GetTotalUserBookmarksCountAsync(userId);

        foreach (var bookmark in bookmarks)
            bookmark.Links = AddBookmarkLinks(bookmark);

        var result = _pagingHelper.CreatePaging(nameof(GetUserBookmarks), page.Number, page.Count, totalItems, bookmarks, new { userId });

        return Ok(result);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateBookmark(int userId, int id, [FromBody] string? note)
    {
        if (note == null)
            return BadRequest("Note content is required.");

        var bookmark = await _bookmarkManager.GetBookmarkAsync(id);
        if (bookmark == null)
            return NotFound();

        if (bookmark.UserId != userId)
            return Forbid();

        var updatedBookmark = await _bookmarkManager.UpdateBookmarkAsync(id, note);
        return updatedBookmark == null ? NotFound() : Ok(updatedBookmark);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBookmark(int userId, int id)
    {
        var bookmark = await _bookmarkManager.GetBookmarkAsync(id);
        if (bookmark == null) 
            return NotFound();

        if (bookmark.UserId != userId)
            return Forbid();

        await _bookmarkManager.DeleteBookmarkAsync(id);
        return NoContent();
    }

    private List<Link> AddBookmarkLinks(BookmarkResult bookmark)
        => [
            new Link {
                Href = _pagingHelper.GetResourceLink(nameof(GetBookmark), new { userId = bookmark.UserId, id = bookmark.BookmarkId }) ?? string.Empty,
                Rel = "self",
                Method = "GET"
            },
            new Link {
                Href = _pagingHelper.GetResourceLink(nameof(UserController.GetUser), new { userId = bookmark.UserId }) ?? string.Empty,
                Rel = "user",
                Method = "GET"
            },
            new Link {
                Href = _pagingHelper.GetResourceLink(nameof(MediaController.Get), new { id = bookmark.MediaId }) ?? string.Empty,
                Rel = "media",
                Method = "GET"
            }
        ];
}