using CitMovie.Models;

namespace CitMovie.Api
{
    [ApiController]
    [Authorize(Policy = "user_scope")]
    [Route("api/users/{userId}/scores")]

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
            [FromQuery] string mediaType = null,
            [FromQuery] int? mediaId = null,
            [FromQuery] string mediaName = null,
            [FromQuery] int page = 0,
            [FromQuery] int pageSize = 10)
        {
            var totalCount = await _userScoreManager.GetTotalUserScoresCountAsync(userId);
            var scores = await _userScoreManager.GetScoresByUserIdAsync(userId, page, pageSize, mediaType, mediaId, mediaName);

            var result = _pagingHelper.CreatePaging(nameof(GetUserScores), page, pageSize, totalCount, scores, new { userId, mediaType, mediaId, mediaName });

            foreach (var score in scores)
            {
                await AddUserScoreLinks(score);
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserScore(int userId, [FromBody] UserScoreCreateRequest userScoreCreateRequest)
        {
            if (userScoreCreateRequest == null)
            {
                return BadRequest("Invalid request data.");
            }

            await _userScoreManager.CreateUserScoreAsync(userId, userScoreCreateRequest.ImdbId, userScoreCreateRequest.Score, userScoreCreateRequest.ReviewText);
            return CreatedAtRoute(nameof(GetUserScores), new { userId }, null);
        }


        private async Task AddUserScoreLinks(UserScoreResult userScore)
        {
            var user = await _userManager.GetUserAsync(userScore.UserId);
            if (user != null)
            {
                userScore.Links.Add(new Link
                {
                    Href = _pagingHelper.GetResourceLink(nameof(UserController.GetUser), new { userId = userScore.UserId }) ?? string.Empty,
                    Rel = "user",
                    Method = "GET"
                });
            }

            var media = _mediaManager.Get(userScore.MediaId);
            if (media != null)
            {
                userScore.Links.Add(new Link
                {
                    Href = _pagingHelper.GetResourceLink(nameof(MediaController.Get), new { id = userScore.MediaId }) ?? string.Empty,
                    Rel = "media",
                    Method = "GET"
                });
            }
        }

    }
}