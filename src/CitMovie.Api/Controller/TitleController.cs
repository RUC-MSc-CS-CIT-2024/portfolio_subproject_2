namespace CitMovie.Api;

[ApiController]
[Route("api/media/{mediaId}/titles")]
[Tags("Media")]
public class TitleController : ControllerBase {
    private readonly ITitleManager _titleManager;
    private readonly ILogger<TitleController> _logger;
    private readonly PagingHelper _pagingHelper;

    public TitleController(ITitleManager titleManager, ILogger<TitleController> logger, PagingHelper pagingHelper)
    {
        _titleManager = titleManager;
        _logger = logger;
        _pagingHelper = pagingHelper;
    }

    [HttpGet(Name = nameof(GetAllTitles))]
    public ActionResult<PagingResult<TitleResult>> GetAllTitles(int mediaId, [FromQuery(Name = "")] PageQueryParameter page) {
        try {
            IEnumerable<TitleResult> results = _titleManager.GetAllForMedia(mediaId, page);
            foreach (var result in results) {
                result.Links = Url.AddTitleLinks(result.Id, mediaId);
            }
            int total = _titleManager.GetTotalTitlesCount(mediaId);
            PagingResult<TitleResult> pagingResult = _pagingHelper.CreatePaging(nameof(GetAllTitles), page.Number, page.Count, total, results);
            return Ok();
        } catch (KeyNotFoundException) {
            return NotFound();
        } catch (Exception e) {
            _logger.LogError(e, "Unexpected error:");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("{titleId}", Name = nameof(GetTitle))]
    public ActionResult<TitleResult> GetTitle(int mediaId, int titleId) {
        try {
            TitleResult result = _titleManager.Get(mediaId, titleId);
            result.Links = Url.AddTitleLinks(titleId, mediaId);
            return Ok(result);
        } catch (KeyNotFoundException) {
            return NotFound();
        } catch (Exception e) {
            _logger.LogError(e, "Unexpected error:");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPost(Name = nameof(CreateTitle))]
    public async Task<IActionResult> CreateTitle(int mediaId, [FromBody] TitleCreateRequest createRequest) {
        try {
            TitleResult newTitle = await _titleManager.CreateAsync(mediaId, createRequest);
            newTitle.Links = Url.AddTitleLinks(newTitle.Id, mediaId);
            var routeValues = new {
                mediaId,
                titleId = newTitle.Id
            };
            return CreatedAtAction(nameof(GetTitle), routeValues, newTitle);
        } catch (Exception ex) {
            _logger.LogInformation(ex, "Unexpected error:");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpDelete("{titleId}", Name = nameof(DeleteTitle))]
    public async Task<IActionResult> DeleteTitle(int mediaId, int titleId) {
        try {
            bool deleted = await _titleManager.DeleteAsync(mediaId, titleId);
            return deleted 
                ? NoContent() 
                : NotFound();
        } catch (Exception ex) {
            _logger.LogInformation(ex, "Unexpected error:");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}