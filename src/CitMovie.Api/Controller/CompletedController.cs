using System.Security.Claims;

namespace CitMovie.Api.Controllers;

[ApiController]
[Route("api/users/{userId}/completed")]
[Authorize(Policy = "user_scope")]
public class CompletedController : ControllerBase
{
    private readonly ICompletedManager _completedManager;

    public CompletedController(ICompletedManager completedManager) =>
        _completedManager = completedManager;

    private int GetUserId() =>
        int.Parse(User.FindFirstValue("user_id") ?? throw new UnauthorizedAccessException("User ID not found"));

    [HttpPost]
    public async Task<IActionResult> CreateCompleted(int userId, [FromBody] CreateCompletedDto createCompletedDto)
    {
        if (userId != GetUserId())
            return Forbid();

        if (createCompletedDto == null)
            return BadRequest("Completed data is required.");

        createCompletedDto.UserId = userId;
        var createdCompleted = await _completedManager.CreateCompletedAsync(createCompletedDto);
        return CreatedAtAction(nameof(GetCompleted), new { userId, id = createdCompleted.CompletedId }, createdCompleted);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCompleted(int userId, int id)
    {
        if (userId != GetUserId())
            return Forbid();

        var completed = await _completedManager.GetCompletedAsync(id);
        if (completed == null)
            return NotFound();

        return completed.UserId != userId ? Forbid() : Ok(completed);
    }

    [HttpGet]
    public async Task<IActionResult> GetUserCompletedItems(int userId, int page = 0, int pageSize = 10)
    {
        if (userId != GetUserId())
            return Forbid();

        var completedItems = await _completedManager.GetUserCompletedItemsAsync(userId, page, pageSize);
        return Ok(completedItems);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateCompleted(int userId, int id, [FromBody] UpdateCompletedDto updateCompletedDto)
    {
        if (userId != GetUserId())
            return Forbid();

        if (updateCompletedDto == null)
            return BadRequest("Update data is required.");

        var completed = await _completedManager.GetCompletedAsync(id);
        if (completed == null)
            return NotFound();

        if (completed.UserId != userId)
            return Forbid();

        var updatedCompleted = await _completedManager.UpdateCompletedAsync(id, updateCompletedDto);
        return updatedCompleted == null ? NotFound() : Ok(updatedCompleted);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCompleted(int userId, int id)
    {
        if (userId != GetUserId())
            return Forbid();

        var completed = await _completedManager.GetCompletedAsync(id);
        if (completed == null)
            return NotFound();

        if (completed.UserId != userId)
            return Forbid();

        var deleted = await _completedManager.DeleteCompletedAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
