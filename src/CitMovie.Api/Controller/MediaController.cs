using CitMovie.Models;

namespace CitMovie.Api;

[ApiController]
[Route("api/media")]
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
    public async Task<IActionResult> Get([FromQuery] MediaQueryParameter queryParameter)
    {
        if (queryParameter.QueryType == MediaQueryType.Basic)
        {
            var mediaList = _mediaManager.GetAllMedia(queryParameter.Page);
            foreach (var media in mediaList)
            {
                await AddMediaBasicLinks(media, queryParameter.Page);
            }
            return Ok(mediaList);
        }

        var searchResults = _mediaManager.Search(queryParameter, GetUserId());
        foreach (var media in searchResults)
        {
            await AddMediaBasicLinks(media, queryParameter.Page);
        }

        return Ok(searchResults);
    }

    [HttpGet("{id}", Name = nameof(Get))]
    public IActionResult Get(int id) {
        try {
            MediaResult m = _mediaManager.Get(id)
            await AddMediaLinks(m, new PageQueryParameter { Number = 1, Count = 10 });
            return Ok(m);
        } catch (KeyNotFoundException) {
            return NotFound();
        } catch (Exception e) {
            _logger.LogError(e, "Unexpected error occured");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("{id}/similar_media", Name = nameof(GetSimilar))]
    public async Task<IActionResult> GetSimilar(int id, [FromQuery] PageQueryParameter pageQuery)
    {
        try
        {
            var similarMedia = _mediaManager.GetSimilar(id, pageQuery);
            foreach (var media in similarMedia)
            {
                await AddMediaBasicLinks(media, pageQuery);
            }

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
    public async Task<IActionResult> GetRelated(int id, [FromQuery] PageQueryParameter pageQuery)
    {
        try
        {
            var relatedMedia = _mediaManager.GetRelated(id, pageQuery);
            foreach (var media in relatedMedia)
            {
                await AddMediaBasicLinks(media, pageQuery);
            }

            var totalItems = await _mediaManager.GetTotalRelatedMediaCountAsync(id);
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

    private int? GetUserId()
    {
        string? userIdString = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
        int? userId = null;
        if (int.TryParse(userIdString, out int parseResult))
            userId = parseResult;
        return userId;
    }

    private async Task AddMediaBasicLinks(MediaBasicResult media, PageQueryParameter pageQuery)
    {
        media.Links.Add(new Link
        {
            Href = Url.Link(nameof(Get), new { id = media.Id }) ?? string.Empty,
            Rel = "self",
            Method = "GET"
        });

        var similarMedia = _mediaManager.GetSimilar(media.Id, pageQuery);
        if (similarMedia != null)
        {
            media.Links.Add(new Link
            {
                Href = Url.Link(nameof(GetSimilar), new { id = media.Id, page = pageQuery.Number, pageSize = pageQuery.Count }) ?? string.Empty,
                Rel = "similar_media",
                Method = "GET"
            });
        }

        var relatedMedia = _mediaManager.GetRelated(media.Id, pageQuery);
        if (relatedMedia != null)
        {
            media.Links.Add(new Link
            {
                Href = Url.Link(nameof(GetRelated), new { id = media.Id, page = pageQuery.Number, pageSize = pageQuery.Count }) ?? string.Empty,
                Rel = "related_media",
                Method = "GET"
            });
        }
    }

    private async Task AddMediaLinks(MediaResult media, PageQueryParameter pageQuery)
    {
        media.Links.Add(new Link
        {
            Href = Url.Link(nameof(Get), new { id = media.Id }) ?? string.Empty,
            Rel = "self",
            Method = "GET"
        });

        var similarMedia = _mediaManager.GetSimilar(media.Id, pageQuery);
        if (similarMedia != null)
        {
            media.Links.Add(new Link
            {
                Href = Url.Link(nameof(GetSimilar), new { id = media.Id, page = pageQuery.Number, pageSize = pageQuery.Count }) ?? string.Empty,
                Rel = "similar_media",
                Method = "GET"
            });
        }

        var relatedMedia = _mediaManager.GetRelated(media.Id, pageQuery);
        if (relatedMedia != null)
        {
            media.Links.Add(new Link
            {
                Href = Url.Link(nameof(GetRelated), new { id = media.Id, page = pageQuery.Number, pageSize = pageQuery.Count }) ?? string.Empty,
                Rel = "related_media",
                Method = "GET"
            });
        }
    }
}