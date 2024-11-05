using System.Security.Claims;
using CitMovie.Models;

namespace CitMovie.Api;

[ApiController]
[Route("api/users/{userId}/bookmarks")]
[Authorize(Policy = "user_scope")]
public class BookmarkController : ControllerBase
{
    private readonly IBookmarkManager _bookmarkManager;
    private readonly IUserManager _userManager;
    private readonly IMediaManager _mediaManager;
    private readonly PagingHelper _pagingHelper;

    public BookmarkController(IBookmarkManager bookmarkManager, IUserManager userManager, IMediaManager mediaManager, PagingHelper pagingHelper)
    {
        _bookmarkManager = bookmarkManager;
        _userManager = userManager;
        _mediaManager = mediaManager;
        _pagingHelper = pagingHelper;
    }

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

    [HttpGet("{id}", Name = nameof(GetBookmark))]
    public async Task<IActionResult> GetBookmark(int userId, int id)
    {
        if (userId != GetUserId())
            return Forbid();

        var bookmark = await _bookmarkManager.GetBookmarkAsync(id);
        if (bookmark == null)
            return NotFound();

        if (bookmark.UserId != userId)
            return Forbid();

        await AddBookmarkLinks(bookmark);

        return Ok(bookmark);
    }

    [HttpGet(Name = nameof(GetUserBookmarks))]
    public async Task<IActionResult> GetUserBookmarks(int userId, int page = 0, int pageSize = 10)
    {
        if (userId != GetUserId())
            return Forbid();

        var bookmarks = await _bookmarkManager.GetUserBookmarksAsync(userId, page, pageSize);
        var totalItems = await _bookmarkManager.GetTotalUserBookmarksCountAsync(userId);

        foreach (var bookmark in bookmarks)
        {
            await AddBookmarkLinks(bookmark);
        }

        var result = _pagingHelper.CreatePaging(nameof(GetUserBookmarks), page, pageSize, totalItems, bookmarks, new { userId });

        return Ok(result);
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

    private async Task AddBookmarkLinks(BookmarkDto bookmark)
    {
        bookmark.Links.Add(new Link
        {
            Href = _pagingHelper.GetResourceLink(nameof(GetBookmark), new { userId = bookmark.UserId, id = bookmark.BookmarkId }) ?? string.Empty,
            Rel = "self",
            Method = "GET"
        });

        var user = await _userManager.GetUserAsync(bookmark.UserId);
        if (user != null)
        {
            bookmark.Links.Add(new Link
            {
                Href = _pagingHelper.GetResourceLink(nameof(UserController.GetUser), new { userId = bookmark.UserId }) ?? string.Empty,
                Rel = "user",
                Method = "GET"
            });
        }

        var media = _mediaManager.Get(bookmark.MediaId);
        if (media != null)
        {
            bookmark.Links.Add(new Link
            {
                Href = _pagingHelper.GetResourceLink(nameof(MediaController.Get), new { id = bookmark.MediaId }) ?? string.Empty,
                Rel = "media",
                Method = "GET"
            });
        }
    }
}