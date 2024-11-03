using System.Security.Claims;

namespace CitMovie.Api;

[ApiController]
[Route("api/users/{userId}/bookmarks")]
[Authorize(Policy = "user_scope")]
public class BookmarkController : ControllerBase
{
    private readonly IBookmarkManager _bookmarkManager;

    public BookmarkController(IBookmarkManager bookmarkManager) =>
        _bookmarkManager = bookmarkManager;

    private int GetUserId() =>
        int.Parse(User.FindFirstValue("user_id") ?? throw new UnauthorizedAccessException("User ID not found"));

    [HttpPost]
    public async Task<IActionResult> CreateBookmark(int userId, [FromBody] CreateBookmarkDto createBookmarkDto)
    {
        if (userId != GetUserId())
            return Forbid();

        if (createBookmarkDto == null)
            return BadRequest("Bookmark data is required.");

        createBookmarkDto.UserId = userId;
        var createdBookmark = await _bookmarkManager.CreateBookmarkAsync(createBookmarkDto);
        return CreatedAtAction(nameof(GetBookmark), new { userId, id = createdBookmark.BookmarkId }, createdBookmark);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookmark(int userId, int id)
    {
        if (userId != GetUserId())
            return Forbid();

        var bookmark = await _bookmarkManager.GetBookmarkAsync(id);
        if (bookmark == null) 
            return NotFound();

        return bookmark.UserId != userId ? Forbid() : Ok(bookmark);
    }

    [HttpGet]
    public async Task<IActionResult> GetUserBookmarks(int userId)
    {
        if (userId != GetUserId())
            return Forbid();

        var bookmarks = await _bookmarkManager.GetUserBookmarksAsync(userId);
        return Ok(bookmarks);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateBookmark(int userId, int id, [FromBody] string? note)
    {
        if (userId != GetUserId())
            return Forbid();

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
        if (userId != GetUserId())
            return Forbid();

        var bookmark = await _bookmarkManager.GetBookmarkAsync(id);
        if (bookmark == null) 
            return NotFound();

        if (bookmark.UserId != userId)
            return Forbid();

        var deleted = await _bookmarkManager.DeleteBookmarkAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
