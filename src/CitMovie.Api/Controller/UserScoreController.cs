namespace CitMovie.Api;

[ApiController]
[Authorize(Policy = "user_scope")]
[Route("api/users/{userId}/scores")]
[Tags("User")]
public class UserScoreController : ControllerBase
{
    private readonly IUserScoreManager _userScoreManager;
    private readonly IUserManager _userManager;
    private readonly IMediaManager _mediaManager;
    private readonly PagingHelper _pagingHelper;

    public UserScoreController(IUserScoreManager userScoreManager, IUserManager userManager, IMediaManager mediaManager, PagingHelper pagingHelper)
    {
        _userScoreManager = userScoreManager;
        _userManager = userManager;
        _mediaManager = mediaManager;
        _pagingHelper = pagingHelper;
    }

    [HttpGet(Name = nameof(GetUserScores))]
    public async Task<ActionResult<IEnumerable<UserScoreResult>>> GetUserScores(
        int userId,
        [FromQuery] PageQueryParameter page,
        [FromQuery] string mediaType = null,
        [FromQuery] int? mediaId = null,
        [FromQuery] string mediaName = null)
    {
        var totalCount = await _userScoreManager.GetTotalUserScoresCountAsync(userId);
        var scores = await _userScoreManager.GetScoresByUserIdAsync(userId, page.Number, page.Count, mediaType, mediaId, mediaName);

        foreach (var score in scores)
            score.Links = AddUserScoreLinks(score);

        var result = _pagingHelper.CreatePaging(nameof(GetUserScores), page.Number, page.Count, totalCount, scores, new { userId, mediaType, mediaId, mediaName });

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUserScore(int userId, [FromBody] UserScoreCreateRequest userScoreCreateRequest)
    {
        if (userScoreCreateRequest == null)
        {
            return BadRequest("Invalid request data.");
        }

        await _userScoreManager.CreateUserScoreAsync(userId, userScoreCreateRequest);
        return CreatedAtRoute(nameof(GetUserScores), new { userId }, null);
    }


    private List<Link> AddUserScoreLinks(UserScoreResult userScore)
        => [ new Link {
                Href = _pagingHelper.GetResourceLink(nameof(UserController.GetUser), new { userId = userScore.UserId }) ?? string.Empty,
                Rel = "user",
                Method = "GET"
            }, 
            new Link {
                Href = _pagingHelper.GetResourceLink(nameof(MediaController.Get), new { id = userScore.MediaId }) ?? string.Empty,
                Rel = "media",
                Method = "GET"
            }
        ];
}