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

    [HttpGet(Name = nameof(Query))]
    public ActionResult<PagingResult<MediaBasicResult>> Query([FromQuery] MediaQueryParameter queryParameter, [FromQuery(Name = "")] PageQueryParameter pageQuery)
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
            if (queryParameter.Keywords is not null && queryParameter.Keywords.Length > 0)
                for (int i = 0; i < queryParameter.Keywords.Length; i++)
                    queryParameter.Keywords[i] = queryParameter.Keywords[i].ToLower();

            mediaResult = _mediaManager.Search(queryParameter, pageQuery, GetUserId());
            totalItems = _mediaManager.GetSearchResultsCount(queryParameter);
        }


        foreach (var media in mediaResult)
            media.Links = Url.AddMediaLinks(media.Id);

        PagingResult<MediaBasicResult> results = _pagingHelper.CreatePaging(
            nameof(Query),
            pageQuery.Number,
            pageQuery.Count,
            totalItems,
            mediaResult);

        return Ok(results);
    }

    [HttpGet("{id}", Name = nameof(GetMedia))]
    public ActionResult<MediaResult> GetMedia(int id) {
        try {
            MediaResult m = _mediaManager.Get(id);
            m.Links = Url.AddMediaLinks(id);
            return Ok(m);
        } catch (KeyNotFoundException) {
            return NotFound();
        } catch (Exception e) {
            _logger.LogError(e, "Unexpected error occured");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("{id}/similar_media", Name = nameof(GetSimilar))]
    public async Task<ActionResult<PagingResult<MediaBasicResult>>> GetSimilar(int id, [FromQuery(Name = "")] PageQueryParameter pageQuery)
    {
        try
        {
            IEnumerable<MediaBasicResult> similarMedia = _mediaManager.GetSimilar(id, pageQuery);
            foreach (var media in similarMedia)
                media.Links = Url.AddMediaLinks(media.Id);

            var totalItems = await _mediaManager.GetTotalSimilarMediaCountAsync(id);
            PagingResult<MediaBasicResult> result = _pagingHelper.CreatePaging(nameof(GetSimilar), pageQuery.Number, pageQuery.Count, totalItems, similarMedia, new { id });

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
    public async Task<ActionResult<PagingResult<MediaBasicResult>>> GetRelated(int id, [FromQuery(Name = "")] PageQueryParameter pageQuery)
    {
        try
        {
            IEnumerable<MediaBasicResult> relatedMedia = _mediaManager.GetRelated(id, pageQuery);
            foreach (var media in relatedMedia)
                media.Links = Url.AddMediaLinks(media.Id);

            int totalItems = await _mediaManager.GetTotalRelatedMediaCountAsync(id);
            PagingResult<MediaBasicResult> result = _pagingHelper.CreatePaging(nameof(GetRelated), pageQuery.Number, pageQuery.Count, totalItems, relatedMedia, new { id });

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
    public async Task<ActionResult<PagingResult<CrewResult>>> GetCrew(int id, [FromQuery(Name = "")] PageQueryParameter pageQuery) {
        
        try {
            IEnumerable<CrewResult> crewResult = await _mediaManager.GetCrewAsync(id, pageQuery);
            foreach (var crew in crewResult)
                crew.Links = Url.AddCrewAndCastLinks(id, crew.PersonId);

            int totalItems = await _mediaManager.GetTotalCrewCountAsync(id);
            PagingResult<CrewResult> result = _pagingHelper.CreatePaging(nameof(GetCrew), pageQuery.Number, pageQuery.Count, totalItems, crewResult, new { id });
            return Ok(result);
        } catch (KeyNotFoundException) {
            return NotFound();
        } catch (Exception e) {
            _logger.LogError(e, "Unexpected error occured");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("{id}/cast", Name = nameof(GetCast))]
    public async Task<ActionResult<PagingResult<CrewResult>>> GetCast(int id, [FromQuery(Name = "")] PageQueryParameter pageQuery) {
        try {
            IEnumerable<CrewResult> castResult = await _mediaManager.GetCastAsync(id, pageQuery);
            foreach (var cast in castResult)
                cast.Links = Url.AddCrewAndCastLinks(id, cast.PersonId);

            int totalItems = await _mediaManager.GetTotalCastCountAsync(id);
            PagingResult<CrewResult> result = _pagingHelper.CreatePaging(nameof(GetCast), pageQuery.Number, pageQuery.Count, totalItems, castResult, new { id });
            return Ok(result);
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
}