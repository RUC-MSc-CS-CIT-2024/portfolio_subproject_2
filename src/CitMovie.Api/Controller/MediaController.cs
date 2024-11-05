namespace CitMovie.Api;

[ApiController]
[Route("api/media")]
public class MediaController : ControllerBase
{
    private readonly IMediaManager _mediaManager;
    private readonly ILogger<MediaController> _logger;

    public MediaController(IMediaManager mediaManager, ILogger<MediaController> logger)
    {
        _mediaManager = mediaManager;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Get([FromQuery] MediaQueryParameter queryParameter)
    {
        if (queryParameter.QueryType == MediaQueryType.Basic) 
            return Ok(_mediaManager.GetAllMedia(queryParameter.Page));

        return Ok(_mediaManager.Search(queryParameter, GetUserId()));
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id) {
        try {
            return Ok(_mediaManager.Get(id));
        } catch (KeyNotFoundException) {
            return NotFound();
        } catch (Exception e) {
            _logger.LogError(e, "Unexpected error occured");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("{id}/similar_media")]
    public IActionResult GetSimilar(int id, [FromQuery] PageQueryParameter pageQuery) {
        try {
           return Ok(_mediaManager.GetSimilar(id, pageQuery));
        } catch (KeyNotFoundException) {
            return NotFound();
        } catch (Exception e) {
            _logger.LogError(e, "Unexpected error occured");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("{id}/related_media")]
    public IActionResult GetRelated(int id, [FromQuery] PageQueryParameter pageQuery) {
        try {
            return Ok(_mediaManager.GetRelated(id, pageQuery));
        } catch (KeyNotFoundException) {
            return NotFound();
        } catch (Exception e) {
            _logger.LogError(e, "Unexpected error occured");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("{id}/crew")]
    public async Task<ActionResult> GetCrew(int id, [FromQuery] PageQueryParameter pageQuery) {
        
        try {
            return Ok(await _mediaManager.GetCrewAsync(id, pageQuery));
        } catch (KeyNotFoundException) {
            return NotFound();
        } catch (Exception e) {
            _logger.LogError(e, "Unexpected error occured");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("{id}/cast")]
    public async Task<IActionResult> GetCast(int id, [FromQuery] PageQueryParameter pageQuery) {
        
        try {
            return Ok(await _mediaManager.GetCastAsync(id, pageQuery));
        } catch (KeyNotFoundException) {
            return NotFound();
        } catch (Exception e) {
            _logger.LogError(e, "Unexpected error occured");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    private int? GetUserId() {
        string? userIdString = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
        int? userId = null;
        if (int.TryParse(userIdString, out int parseResult))
            userId = parseResult;
        return userId;
    }
}
