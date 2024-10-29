using CitMovie.Business.Managers;
using CitMovie.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CitMovie.Api.Controllers
{
    [ApiController]
    [Route("api/bookmarks")]
    [Authorize(Policy = "user_scope")]
    public class BookmarkController : ControllerBase
    {
        private readonly IBookmarkManager _bookmarkManager;

        public BookmarkController(IBookmarkManager bookmarkManager) =>
            _bookmarkManager = bookmarkManager;

        private int GetUserId() =>
            int.Parse(User.FindFirstValue("user_id") ?? throw new UnauthorizedAccessException("User ID not found"));


        [HttpPost]
        public async Task<IActionResult> CreateBookmark([FromBody] CreateBookmarkDto createBookmarkDto)
        {
            if (createBookmarkDto == null)
                return BadRequest("Bookmark data is required.");

            createBookmarkDto.UserId = GetUserId();
            var createdBookmark = await _bookmarkManager.CreateBookmarkAsync(createBookmarkDto);
            return CreatedAtAction(nameof(GetBookmark), new { id = createdBookmark.BookmarkId }, createdBookmark);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookmark(int id)
        {
            var bookmark = await _bookmarkManager.GetBookmarkAsync(id);
            return bookmark == null || bookmark.UserId != GetUserId() ? Unauthorized() : Ok(bookmark);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserBookmarks()
        {
            var userId = GetUserId();
            var bookmarks = await _bookmarkManager.GetUserBookmarksAsync(userId);
            return Ok(bookmarks);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBookmark(int id, [FromBody] string? note)
        {
            if (note == null)
                return BadRequest("Note content is required.");

            var bookmark = await _bookmarkManager.GetBookmarkAsync(id);
            if (bookmark == null || bookmark.UserId != GetUserId())
                return Unauthorized();

            var updatedBookmark = await _bookmarkManager.UpdateBookmarkAsync(id, note);
            return updatedBookmark == null ? NotFound() : Ok(updatedBookmark);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookmark(int id)
        {
            var bookmark = await _bookmarkManager.GetBookmarkAsync(id);
            if (bookmark == null || bookmark.UserId != GetUserId())
                return Unauthorized();

            var deleted = await _bookmarkManager.DeleteBookmarkAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
