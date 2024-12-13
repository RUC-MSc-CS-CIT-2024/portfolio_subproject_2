namespace CitMovie.Api;

[ApiController]
[Authorize(Policy = "user_scope")]
[Route("api/users/{userId}/following")]
[Tags("User")]
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
    public async Task<IActionResult> GetFollowings(int userId, [FromQuery(Name = "")] PageQueryParameter page)
    {
        var followings = await _followManager.GetFollowingsAsync(userId, page.Number, page.Count);
        var totalItems = await _followManager.GetTotalFollowingsCountAsync(userId);

        foreach (var following in followings)
            following.Links = Url.AddFollowLinks(following.PersonId, userId, following.FollowingId);

        var result = _pagingHelper.CreatePaging(nameof(GetFollowings), page.Number, page.Count, totalItems, followings, new { userId });
        return Ok(result);
    }


    [HttpPost(Name = nameof(CreateFollow))]
    public async Task<ActionResult<FollowResult>> CreateFollow(int userId, [FromBody] FollowCreateRequest followCreateRequest)
    {
        var follow = await _followManager.CreateFollowAsync(userId, followCreateRequest.PersonId);
        follow.Links = Url.AddFollowLinks(follow.PersonId, userId, follow.FollowingId);
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
}
