namespace CitMovie.Api;

[ApiController]
[Route("api/media/{mediaId}/releases/{releaseId}/promotional_media")]
[Tags("Media")]
public class PromotionalMediaController : ControllerBase
{
    private readonly IPromotionalMediaManager _manager;
    private readonly PagingHelper _pagingHelper;

    public PromotionalMediaController(IPromotionalMediaManager manager, PagingHelper pagingHelper)
    {
        _manager = manager;
        _pagingHelper = pagingHelper;
    }

    [HttpGet("/api/media/{mediaId}/promotional_media", Name = nameof(GetPromotionalMediaofMedia))]
    public async Task<IActionResult> GetPromotionalMediaofMedia(int mediaId, [FromQuery(Name = "")] PageQueryParameter page)
    {
        try
        {
            var items = await _manager.GetPromotionalMediaOfMediaAsync(mediaId, page.Number, page.Count);
            var totalItems = await _manager.GetPromotionalMediaCountAsync(mediaId, "media");

            foreach (var promotionalMedia in items)
                promotionalMedia.Links = AddPromotionalMediaLinks(promotionalMedia, mediaId, promotionalMedia.ReleaseId);

            var result = _pagingHelper.CreatePaging(
                nameof(GetPromotionalMediaofMedia),
                page.Number, page.Count,
                totalItems,
                items,
                new { mediaId }
            );

            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet(Name = nameof(GetPromotionalMediaOfRelease))]
    public async Task<IActionResult> GetPromotionalMediaOfRelease(int mediaId, int releaseId, [FromQuery(Name = "")] PageQueryParameter page)
    {
        try
        {
            var items = await _manager.GetPromotionalMediaOfReleaseAsync(mediaId, releaseId, page.Number, page.Count);
            var totalItems = await _manager.GetPromotionalMediaCountAsync(releaseId, "release");

            foreach (var promotionalMedia in items)
                promotionalMedia.Links = AddPromotionalMediaLinks(promotionalMedia, mediaId, promotionalMedia.ReleaseId);

            var result = _pagingHelper.CreatePaging(
                nameof(GetPromotionalMediaOfRelease),
                page.Number, page.Count,
                totalItems,
                items,
                new { mediaId, releaseId }
            );

            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

    }

    [HttpGet("{id}", Name = nameof(GetPromotionalMediaById))]
    public async Task<IActionResult> GetPromotionalMediaById(int id, int mediaId, int releaseId)
    {
        try
        {
            var result = await _manager.GetPromotionalMediaByIdAsync(id, mediaId, releaseId);

            result.Links = AddPromotionalMediaLinks(result, mediaId, releaseId);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePromotionalMedia(int mediaId, int releaseId, int id)
    {
        try
        {
            bool deleted = await _manager.DeletePromotionalMediaAsync(mediaId, releaseId, id);
            return deleted
                ? NoContent()
                : NotFound("Promotional media not found");
        }
        catch
        {
            return BadRequest("Delete failed");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreatePromotionalMedia([FromRoute] int mediaId, [FromRoute] int releaseId, [FromBody] PromotionalMediaCreateRequest model)
    {
        try
        {
            var response = await _manager.CreatePromotionalMediaAsync(mediaId, releaseId, model);
            response.Links = AddPromotionalMediaLinks(response, mediaId, releaseId);
            return CreatedAtAction(nameof(CreatePromotionalMedia), response);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    private List<Link> AddPromotionalMediaLinks(PromotionalMediaResult promotionalMedia, int mediaId, int releaseId)
        => [ new Link {
                Href = _pagingHelper.GetResourceLink(nameof(MediaController.Get), new { id = mediaId }) ?? string.Empty,
                Rel = "media",
                Method = "GET"
            },
            new Link {
                Href = _pagingHelper.GetResourceLink(nameof(ReleaseController.GetReleaseOfMediaById), new { mediaId, id = releaseId }) ?? string.Empty,
                Rel = "release",
                Method = "GET"
            },
            new Link {
                Href = _pagingHelper.GetResourceLink(nameof(GetPromotionalMediaOfRelease), new { mediaId, releaseId, promotionalMediaId = promotionalMedia.PromotionalMediaId }) ?? string.Empty,
                Rel = "self",
                Method = "GET"
            }
        ];
}