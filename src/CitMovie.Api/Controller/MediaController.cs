namespace CitMovie.Api;

[ApiController]
[Route("api/media")]
[Tags("Media")]
public class MediaController : ControllerBase
{
    private readonly IMediaManager _mediaManager;
    private readonly ILogger<MediaController> _logger;
    private readonly PagingHelper _pagingHelper;

    public MediaController(IMediaManager mediaManager, ILogger<MediaController> logger, PagingHelper pagingHelper)
    {
        _mediaManager = mediaManager;
        _logger = logger;
        _pagingHelper = pagingHelper;
    }

    [HttpGet]
    public IActionResult Get([FromQuery] MediaQueryParameter queryParameter, [FromQuery(Name = "")] PageQueryParameter pageQuery)
    {
        IEnumerable<MediaBasicResult> mediaResult;
        int totalItems = 0;
        if (queryParameter.QueryType == MediaQueryType.All)
        {
            mediaResult = _mediaManager.GetAllMedia(pageQuery);
            totalItems = _mediaManager.GetTotalMediaCount();
        }
        else
        {
            mediaResult = _mediaManager.Search(queryParameter, pageQuery, GetUserId());
            totalItems = _mediaManager.GetSearchResultsCount(queryParameter);
        }


        foreach (var media in mediaResult)
            media.Links = GenerateLinks(media.Id);

        var results = _pagingHelper.CreatePaging(
            nameof(Get),
            pageQuery.Number,
            pageQuery.Count,
            totalItems,
            mediaResult);

        return Ok(results);
    }

    [HttpGet("{id}", Name = nameof(Get))]
    public IActionResult Get(int id) {
        try {
            MediaResult m = _mediaManager.Get(id);
            m.Links = GenerateLinks(id);
            return Ok(m);
        } catch (KeyNotFoundException) {
            return NotFound();
        } catch (Exception e) {
            _logger.LogError(e, "Unexpected error occured");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("{id}/similar_media", Name = nameof(GetSimilar))]
    public async Task<IActionResult> GetSimilar(int id, [FromQuery(Name = "")] PageQueryParameter pageQuery)
    {
        try
        {
            IEnumerable<MediaBasicResult> similarMedia = _mediaManager.GetSimilar(id, pageQuery);
            foreach (var media in similarMedia)
                media.Links = GenerateLinks(media.Id);

            var totalItems = await _mediaManager.GetTotalSimilarMediaCountAsync(id);
            var result = _pagingHelper.CreatePaging(nameof(GetSimilar), pageQuery.Number, pageQuery.Count, totalItems, similarMedia, new { id });

            return Ok(result);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Unexpected error occurred");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("{id}/related_media", Name = nameof(GetRelated))]
    public async Task<IActionResult> GetRelated(int id, [FromQuery(Name = "")] PageQueryParameter pageQuery)
    {
        try
        {
            IEnumerable<MediaBasicResult> relatedMedia = _mediaManager.GetRelated(id, pageQuery);
            foreach (var media in relatedMedia)
                media.Links = GenerateLinks(media.Id);

            int totalItems = await _mediaManager.GetTotalRelatedMediaCountAsync(id);
            var result = _pagingHelper.CreatePaging(nameof(GetRelated), pageQuery.Number, pageQuery.Count, totalItems, relatedMedia, new { id });

            return Ok(result);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Unexpected error occurred");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("{id}/crew", Name = nameof(GetCrew))]
    public async Task<ActionResult> GetCrew(int id, [FromQuery(Name = "")] PageQueryParameter pageQuery) {
        
        try {
            IEnumerable<CrewResult> crewResult = await _mediaManager.GetCrewAsync(id, pageQuery);
            foreach (var media in crewResult)
                media.Links = GenerateLinks(media.Id);

            int totalItems = await _mediaManager.GetTotalCrewCountAsync(id);
            var result = _pagingHelper.CreatePaging(nameof(GetRelated), pageQuery.Number, pageQuery.Count, totalItems, crewResult, new { id });
            return Ok(result);
        } catch (KeyNotFoundException) {
            return NotFound();
        } catch (Exception e) {
            _logger.LogError(e, "Unexpected error occured");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("{id}/cast", Name = nameof(GetCast))]
    public async Task<IActionResult> GetCast(int id, [FromQuery(Name = "")] PageQueryParameter pageQuery) {
        try {
            IEnumerable<CrewResult> castResult = await _mediaManager.GetCastAsync(id, pageQuery);
            foreach (var media in castResult)
                media.Links = GenerateLinks(media.Id);

            int totalItems = await _mediaManager.GetTotalCastCountAsync(id);
            var result = _pagingHelper.CreatePaging(nameof(GetRelated), pageQuery.Number, pageQuery.Count, totalItems, castResult, new { id });
            return Ok();
        } catch (KeyNotFoundException) {
            return NotFound();
        } catch (Exception e) {
            _logger.LogError(e, "Unexpected error occured");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    private int? GetUserId()
    {
        string? userIdString = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
        int? userId = null;
        if (int.TryParse(userIdString, out int parseResult))
            userId = parseResult;
        return userId;
    }

    private List<Link> GenerateLinks(int mediaId)
        => [
            new Link {
                Href = Url.Link(nameof(Get), new { id = mediaId }) ?? string.Empty,
                Rel = "self",
                Method = "GET"
            },
            new Link {
                Href = Url.Link(nameof(GetSimilar), new { id = mediaId }) ?? string.Empty,
                Rel = "similar_media",
                Method = "GET"
            },
            new Link {
                Href = Url.Link(nameof(GetRelated), new { id = mediaId }) ?? string.Empty,
                Rel = "related_media",
                Method = "GET"
            }
        ];
}