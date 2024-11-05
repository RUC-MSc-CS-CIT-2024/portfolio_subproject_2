using CitMovie.Models;

namespace CitMovie.Api;

[ApiController]
[Authorize(Policy = "user_scope")]
[Route("api/users/{userId}/search_history")]
[Tags("User")]
public class SearchHistoryController : ControllerBase
{
    private readonly ISearchHistoryManager _searchHistoryManager;
    private readonly IUserManager _userManager;
    private readonly PagingHelper _pagingHelper;

    public SearchHistoryController(ISearchHistoryManager searchHistoryManager, IUserManager userManager, PagingHelper pagingHelper)
    {
        _searchHistoryManager = searchHistoryManager;
        _userManager = userManager;
        _pagingHelper = pagingHelper;
    }

    [HttpGet(Name = nameof(GetUserSearchHistories))]
    public async Task<IActionResult> GetUserSearchHistories(int userId, [FromQuery] PageQueryParameter page)
    {
        var searchHistories = await _searchHistoryManager.GetUserSearchHistoriesAsync(userId, page.Number, page.Count);
        var total_items = await _searchHistoryManager.GetUsersTotalSearchHistoriesCountAsync(userId);

        foreach (var searchHistory in searchHistories)
        {
            await AddSearchHistoryUserLink(searchHistory);
        }

        var result = _pagingHelper.CreatePaging(nameof(GetUserSearchHistories), page.Number, page.Count, total_items, searchHistories, new { userId });

        return Ok(result);
    }

    [HttpDelete(Name = nameof(DeleteUserSearchHistories))]
    public async Task<IActionResult> DeleteUserSearchHistories(int userId, int searchHistoryId)
    {
        var result = await _searchHistoryManager.DeleteUsersSearchHistoriesAsync(userId, searchHistoryId);
        if (result)
        {
            return Ok();
        }
        return NotFound();
    }

    private async Task AddSearchHistoryUserLink(SearchHistoryResult searchHistoryResult)
    {
        var user = await _userManager.GetUserAsync(searchHistoryResult.UserId);
        if (user != null)
        {
            searchHistoryResult.Links.Add(new Link
            {
                Href = _pagingHelper.GetResourceLink(nameof(UserController.GetUser), new { userId = searchHistoryResult.UserId }) ?? string.Empty,
                Rel = "user",
                Method = "GET"
            });
        }
    }
}