using CitMovie.Models;

namespace CitMovie.Api;

[ApiController]
[Authorize(Policy = "user_scope")]
[Route("api/users/{userId}/following")]
public class FollowController : ControllerBase
{
    private readonly IFollowManager _followManager;
    private readonly IPersonManager _personManager;
    private readonly PagingHelper _pagingHelper;

    public FollowController(IFollowManager followManager, IPersonManager personManager, PagingHelper pagingHelper)
    {
        _followManager = followManager;
        _personManager = personManager;
        _pagingHelper = pagingHelper;
    }

    [HttpGet(Name = nameof(GetFollowings))]
    public async Task<IActionResult> GetFollowings(int userId, int page = 0, int pageSize = 10)
    {
        var followings = await _followManager.GetFollowingsAsync(userId, page, pageSize);
        var totalItems = await _followManager.GetTotalFollowingsCountAsync(userId);

        foreach (var following in followings)
        {
            await AddFollowingLinks(following);
        }

        var result = _pagingHelper.CreatePaging(nameof(GetFollowings), page, pageSize, totalItems, followings, new { userId });
        return Ok(result);
    }


    [HttpPost]
    public async Task<ActionResult<FollowResult>> CreateFollow(int userId, [FromBody] FollowCreateRequest followCreateRequest)
    {
        var follow = await _followManager.CreateFollowAsync(userId, followCreateRequest.PersonId);
        return CreatedAtAction(nameof(GetFollowings), new { userId }, follow);
    }

    [HttpDelete("{followingId}")]
    public async Task<IActionResult> RemoveFollowing(int userId, int followingId)
    {
        var result = await _followManager.RemoveFollowingAsync(userId, followingId);
        if (!result)
            return NotFound();

        return NoContent();
    }
    private async Task AddFollowingLinks(FollowResult followResult)
    {

        var person = await _personManager.GetPersonByIdAsync(followResult.PersonId);
        if (person != null)
        {
            followResult.Links.Add(new Link
            {
                Href = _pagingHelper.GetResourceLink(nameof(PersonController.GetPersonById), new { id = followResult.PersonId }) ?? string.Empty,
                Rel = "person",
                Method = "GET"
            });
        }
    }

}
