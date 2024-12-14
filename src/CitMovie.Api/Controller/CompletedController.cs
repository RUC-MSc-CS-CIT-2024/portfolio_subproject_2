namespace CitMovie.Api;

[ApiController]
[Route("api/users/{userId}/completed")]
[Authorize(Policy = "user_scope")]
[Tags("User")]
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

    [HttpPost()]
    public async Task<IActionResult> MoveBookmarkToCompleted(int userId, [FromBody] CompletedCreateRequest createCompletedDto)
    {
        if (createCompletedDto == null)
            return BadRequest("Completed data is required.");

        var completedItem = await _completedManager.CreateBookmarkAsync(userId, createCompletedDto);
        
        return CreatedAtAction(nameof(GetCompleted), new { userId, id = completedItem.CompletedId }, completedItem);
    }

    [HttpGet("{id}", Name = nameof(GetCompleted))]
    public async Task<IActionResult> GetCompleted(int userId, int id)
    {
        var completed = await _completedManager.GetCompletedAsync(id);
        if (completed == null)
            return NotFound();

        if (completed.UserId != userId)
            return Forbid();

        completed.Links = Url.AddCompletedLinks(completed.CompletedId, completed.MediaId, userId);

        return Ok(completed);
    }

    [HttpGet(Name = nameof(GetUserCompleted))]
    public async Task<IActionResult> GetUserCompleted(int userId, [FromQuery(Name = "")] PageQueryParameter page)
    {
        var completedItems = await _completedManager.GetUserCompletedItemsAsync(userId, page.Number, page.Count);
        var totalItems = await _completedManager.GetTotalUserCompletedCountAsync(userId);

        foreach (var completed in completedItems)
            completed.Links = Url.AddCompletedLinks(completed.CompletedId, completed.MediaId, userId);

        var result = _pagingHelper.CreatePaging(nameof(GetUserCompleted), page.Number, page.Count, totalItems, completedItems, new { userId });

        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCompleted(int userId, int id, [FromBody] UpdateCompletedDto updateCompletedDto)
    {
        if (updateCompletedDto == null)
            return BadRequest("Update data is required.");

        var updatedCompleted = await _completedManager.UpdateCompletedAsync(id, updateCompletedDto);
        return updatedCompleted == null ? NotFound() : Ok(updatedCompleted);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCompleted(int userId, int id)
    {
        var completed = await _completedManager.GetCompletedAsync(id);
        if (completed == null)
            return NotFound();

        if (completed.UserId != userId)
            return Forbid();

        var deleted = await _completedManager.DeleteCompletedAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
