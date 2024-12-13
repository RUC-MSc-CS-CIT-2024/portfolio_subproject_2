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
        createdBookmark.Links = Url.AddBookmarkLinks(createdBookmark.BookmarkId, createdBookmark.MediaId, userId);
        return CreatedAtAction(nameof(GetBookmark), new { userId, id = createdBookmark.BookmarkId }, createdBookmark);
    }

    [HttpPost("{id}/move")]
    public async Task<IActionResult> MoveBookmarkToCompleted(int userId, int id, [FromBody] BookmarkMoveRequest bookmarkMoveRequest)
    {
        if (bookmarkMoveRequest == null)
            return BadRequest("Completed data is required.");

        try {
            var completedItem = await _completedManager.MoveBookmarkToCompletedAsync(userId, id, bookmarkMoveRequest);
            completedItem.Links = Url.AddCompletedLinks(completedItem.CompletedId, completedItem.MediaId, userId);
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

        bookmark.Links = Url.AddBookmarkLinks(id, bookmark.MediaId, userId);

        return Ok(bookmark);
    }

    [HttpGet(Name = nameof(GetUserBookmarks))]
    public async Task<IActionResult> GetUserBookmarks(int userId, [FromQuery(Name = "")] PageQueryParameter page)
    {
        var bookmarks = await _bookmarkManager.GetUserBookmarksAsync(userId, page.Number, page.Count);
        var totalItems = await _bookmarkManager.GetTotalUserBookmarksCountAsync(userId);

        foreach (var bookmark in bookmarks)
            bookmark.Links = Url.AddBookmarkLinks(bookmark.BookmarkId, bookmark.MediaId, userId);

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

        BookmarkResult? updatedBookmark = await _bookmarkManager.UpdateBookmarkAsync(id, note);
        if (updatedBookmark is null)
            return NotFound();
        
        updatedBookmark.Links = Url.AddBookmarkLinks(id, updatedBookmark.MediaId, userId);
        return Ok(updatedBookmark);
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
}