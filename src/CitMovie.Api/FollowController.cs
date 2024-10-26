using CitMovie.Business;
using CitMovie.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace CitMovie.Api;

[ApiController]
[Route("api/users/{userId}/following")]
public class FollowController : ControllerBase
{
    private readonly LinkGenerator _linkGenerator;
    private readonly FollowService _followService;

    public FollowController(LinkGenerator linkGenerator, FollowService followService)
    {
        _linkGenerator = linkGenerator;
        _followService = followService;
    }

    [HttpGet(Name = nameof(GetFollowings))]
    public async Task<IActionResult> GetFollowings(int userId, int page = 0, int pageSize = 10)
    {
        var followings = await _followService.GetFollowings(userId, page, pageSize);
        var totalItems = await _followService.GetTotalFollowingsCount(userId);

        object result = CreatePaging(
            nameof(GetFollowings),
            userId,
            page,
            pageSize,
            totalItems,
            followings);
        return Ok(result);
    }


    [HttpPost]
    public async Task<ActionResult<FollowDto>> CreateFollow(int userId, [FromBody] CreateFollowDto createFollowDto)
    {
        var follow = await _followService.CreateFollow(userId, createFollowDto.PersonId);
        return CreatedAtAction(nameof(GetFollowings), new { userId }, follow);
    }

    [HttpDelete("{followingId}")]
    public async Task<IActionResult> RemoveFollowing(int userId, int followingId)
    {
        var result = await _followService.RemoveFollowing(userId, followingId);
        if (!result)
            return NotFound();

        return NoContent();
    }


    // HATEOS And Pagingination
    private string? GetLink(string linkName, int userId, int page, int pageSize)
    {
        var uri = _linkGenerator.GetUriByName(
                    HttpContext,
                    linkName,
                    new { userId, page, pageSize }
                    );
        return uri;
    }

    private string? GetUrl(int userId, int followingId)
    {
        return _linkGenerator.GetUriByName(
            HttpContext,
            nameof(GetFollowings),
            new { userId, followingId }
        );
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

        var result = new
        {
            CurPage = curPage,
            NextPage = nextPage,
            PrevPage = prevPage,
            NumberOfItems = total,
            NumberPages = numberOfPages,
            Items = items
        };
        return result;
    }
}
