using CitMovie.Models;

namespace CitMovie.Api;

[ApiController]
[Authorize(Policy = "user_scope")]
[Route("api/users/{userId}/search_history")]
public class SearchHistoryController : ControllerBase
{
    private readonly ISearchHistoryManager _searchHistoryManager;
    private readonly IUserManager _userManager;
    private readonly LinkGenerator _linkGenerator;

    public SearchHistoryController(ISearchHistoryManager searchHistoryManager, IUserManager userManager, LinkGenerator linkGenerator)
    {
        _searchHistoryManager = searchHistoryManager;
        _userManager = userManager;
        _linkGenerator = linkGenerator;
    }

    [HttpGet(Name = nameof(GetUserSearchHistories))]
    public async Task<IActionResult> GetUserSearchHistories(int userId, int page = 0, int pageSize = 10)
    {
        var searchHistories = await _searchHistoryManager.GetUserSearchHistoriesAsync(userId, page, pageSize);
        var total_items = await _searchHistoryManager.GetUsersTotalSearchHistoriesCountAsync(userId);

        foreach (var searchHistory in searchHistories)
        {
            await AddSearchHistoryUserLink(searchHistory);
        }

        var result = CreatePaging(nameof(GetUserSearchHistories), userId, page, pageSize, total_items, searchHistories);

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


    private string? GetLink(string linkName, int userId, int page, int pageSize)
    {
        var uri = _linkGenerator.GetUriByName(
            HttpContext,
            linkName,
            new { userId, page, pageSize }
        );
        return uri;
    }

    private object CreatePaging<T>(string linkName, int userId, int page, int pageSize, int total, IEnumerable<T?> items)
    {
        var numberOfPages = (int)Math.Ceiling(total / (double)pageSize);

        var curPage = GetLink(linkName, userId, page, pageSize);

        var nextPage = page < numberOfPages - 1
            ? GetLink(linkName, userId, page + 1, pageSize)
            : null;

        var prevPage = page > 0
            ? GetLink(linkName, userId, page - 1, pageSize)
            : null;

        return new
        {
            curPage,
            nextPage,
            prevPage,
            numberOfPages,
            total,
            items
        };
    }

    private async Task AddSearchHistoryUserLink(SearchHistoryResult searchHistoryResult)
    {
        var user = await _userManager.GetUserAsync(searchHistoryResult.UserId);
        if (user != null)
        {
            searchHistoryResult.Links.Add(new Link
            {
                Href = HttpContext != null ? _linkGenerator.GetUriByName(HttpContext, "GetUser", new { userId = searchHistoryResult.UserId }) : string.Empty,
                Rel = "user",
                Method = "GET"
            });
        }
    }
}
