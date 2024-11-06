using System.Security.Claims;
using CitMovie.Models;

namespace CitMovie.Api.Controllers;

[ApiController]
[Route("api/users/{userId}/completed")]
[Authorize(Policy = "user_scope")]
public class CompletedController : ControllerBase
{
    private readonly ICompletedManager _completedManager;
    private readonly IUserManager _userManager;
    private readonly IMediaManager _mediaManager;
    private readonly PagingHelper _pagingHelper;

    public CompletedController(ICompletedManager completedManager, IUserManager userManager, IMediaManager mediaManager, PagingHelper pagingHelper)
    {
        _completedManager = completedManager;
        _userManager = userManager;
        _mediaManager = mediaManager;
        _pagingHelper = pagingHelper;
    }

    private int GetUserId() =>
        int.Parse(User.FindFirstValue("user_id") ?? throw new UnauthorizedAccessException("User ID not found"));

    [HttpPost("move")]
    public async Task<IActionResult> MoveBookmarkToCompleted(int userId, [FromBody] MoveCompletedDto moveCompletedDto)
    {
        if (userId != GetUserId())
            return Forbid();

        if (moveCompletedDto == null)
            return BadRequest("Move completed data is required.");

        moveCompletedDto.UserId = userId;
        var completedItem = await _completedManager.MoveBookmarkToCompletedAsync(userId, moveCompletedDto.MediaId, moveCompletedDto.Rewatchability, moveCompletedDto.Note);
        
        return CreatedAtAction(nameof(GetCompleted), new { userId, id = completedItem.CompletedId }, completedItem);
    }

    [HttpGet("{id}", Name = nameof(GetCompleted))]
    public async Task<IActionResult> GetCompleted(int userId, int id)
    {
        if (userId != GetUserId())
            return Forbid();

        var completed = await _completedManager.GetCompletedAsync(id);
        if (completed == null)
            return NotFound();

        if (completed.UserId != userId)
            return Forbid();

        await AddCompletedLinks(completed);

        return Ok(completed);
    }

    [HttpGet(Name = nameof(GetUserCompleted))]
    public async Task<IActionResult> GetUserCompleted(int userId, int page = 0, int pageSize = 10)
    {
        if (userId != GetUserId())
            return Forbid();

        var completedItems = await _completedManager.GetUserCompletedItemsAsync(userId, page, pageSize);
        var totalItems = await _completedManager.GetTotalUserCompletedCountAsync(userId);

        foreach (var completed in completedItems)
        {
            await AddCompletedLinks(completed);
        }

        var result = _pagingHelper.CreatePaging(nameof(GetUserCompleted), page, pageSize, totalItems, completedItems, new { userId });

        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCompleted(int userId, int id, [FromBody] UpdateCompletedDto updateCompletedDto)
    {
        if (userId != GetUserId())
            return Forbid();

        if (updateCompletedDto == null)
            return BadRequest("Update data is required.");

        var updatedCompleted = await _completedManager.UpdateCompletedAsync(id, updateCompletedDto);
        return updatedCompleted == null ? NotFound() : Ok(updatedCompleted);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCompleted(int userId, int id)
    {
        if (userId != GetUserId())
            return Forbid();

        var deleted = await _completedManager.DeleteCompletedAsync(id);
        return deleted ? NoContent() : NotFound();
    }

    private async Task AddCompletedLinks(CompletedDto completed)
    {
        completed.Links.Add(new Link
        {
            Href = _pagingHelper.GetResourceLink(nameof(GetCompleted), new { userId = completed.UserId, id = completed.CompletedId }) ?? string.Empty,
            Rel = "self",
            Method = "GET"
        });

        var user = await _userManager.GetUserAsync(completed.UserId);
        if (user != null)
        {
            completed.Links.Add(new Link
            {
                Href = _pagingHelper.GetResourceLink(nameof(UserController.GetUser), new { userId = completed.UserId }) ?? string.Empty,
                Rel = "user",
                Method = "GET"
            });
        }

        var media = _mediaManager.Get(completed.MediaId);
        if (media != null)
        {
            completed.Links.Add(new Link
            {
                Href = _pagingHelper.GetResourceLink(nameof(MediaController.Get), new { id = completed.MediaId }) ?? string.Empty,
                Rel = "media",
                Method = "GET"
            });
        }
    }
}
