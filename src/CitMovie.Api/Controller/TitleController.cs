namespace CitMovie.Api;

[ApiController]
[Route("api/media/{mediaId}/titles")]
[Tags("Media")]
public class TitleController : ControllerBase {
    private readonly ITitleManager _titleManager;
    private readonly ILogger<TitleController> _logger;

    public TitleController(ITitleManager titleManager, ILogger<TitleController> logger)
    {
        _titleManager = titleManager;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult GetAll(int mediaId, [FromQuery(Name = "")] PageQueryParameter page) {
        try {
            return Ok(_titleManager.GetAllForMedia(mediaId, page));
        } catch (KeyNotFoundException) {
            return NotFound();
        } catch (Exception e) {
            _logger.LogError(e, "Unexpected error:");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("{titleId}")]
    public IActionResult Get(int mediaId, int titleId) {
        try {
            return Ok(_titleManager.Get(mediaId, titleId));
        } catch (KeyNotFoundException) {
            return NotFound();
        } catch (Exception e) {
            _logger.LogError(e, "Unexpected error:");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create(int mediaId, [FromBody] TitleCreateRequest createRequest) {
        try {
            TitleResult newTitle = await _titleManager.CreateAsync(mediaId, createRequest);
            var routeValues = new {
                mediaId,
                titleId = newTitle.Id
            };
            return CreatedAtAction(nameof(Get), routeValues, newTitle);
        } catch (Exception ex) {
            _logger.LogInformation(ex, "Unexpected error:");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpDelete("{titleId}")]
    public async Task<IActionResult> Delete(int mediaId, int titleId) {
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