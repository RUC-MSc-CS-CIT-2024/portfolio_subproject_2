namespace CitMovie.Api
{
    [ApiController]
    //[Authorize(Policy = "user_scope")]
    [Route("api/users/{userId}/scores")]

    public class UserScoreController : ControllerBase
    {
        private readonly IUserScoreManager _userScoreManager;
        private readonly LinkGenerator _linkGenerator;

        public UserScoreController(IUserScoreManager userScoreManager, LinkGenerator linkGenerator)
        {
            _userScoreManager = userScoreManager;
            _linkGenerator = linkGenerator;
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

            var result = CreatePaging(
                nameof(GetUserScores),
                userId,
                mediaType,
                mediaId ?? 0,
                mediaName,
                page,
                pageSize,
                totalCount,
                scores
            );

            return Ok(result);
        }

        // HATEOAS and Pagination
        private string? GetLink(string linkName, int userId, string mediaType, int mediaId, string mediaName, int page, int pageSize)
        {
            var uri = _linkGenerator.GetUriByName(
                        HttpContext,
                        linkName,
                        new { userId, mediaType, mediaId, mediaName, page, pageSize }
                        );
            return uri;
        }

        private object CreatePaging<T>(string linkName, int userId, string mediaType, int mediaId, string mediaName, int page, int pageSize, int total, IEnumerable<T?> items)
        {
            var numberOfPages = (int)Math.Ceiling(total / (double)pageSize);

            var curPage = GetLink(linkName, userId, mediaType, mediaId, mediaName, page, pageSize);

            var nextPage = page < numberOfPages - 1
                ? GetLink(linkName, userId, mediaType, mediaId, mediaName, page + 1, pageSize)
                : null;

            var prevPage = page > 0
                ? GetLink(linkName, userId, mediaType, mediaId, mediaName, page - 1, pageSize)
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
}