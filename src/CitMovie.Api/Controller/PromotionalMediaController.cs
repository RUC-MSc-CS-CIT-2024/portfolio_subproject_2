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
                promotionalMedia.Links = Url.AddPromotionalMediaLinks(promotionalMedia.PromotionalMediaId, mediaId, promotionalMedia.ReleaseId);

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
                promotionalMedia.Links = Url.AddPromotionalMediaLinks(promotionalMedia.PromotionalMediaId, mediaId, promotionalMedia.ReleaseId);

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

            result.Links = Url.AddPromotionalMediaLinks(id, mediaId, releaseId);
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
    public async Task<IActionResult> CreatePromotionalMedia(int mediaId, int releaseId, [FromBody] PromotionalMediaCreateRequest model)
    {
        try
        {
            var response = await _manager.CreatePromotionalMediaAsync(mediaId, releaseId, model);
            response.Links = Url.AddPromotionalMediaLinks(response.PromotionalMediaId, mediaId, releaseId);
            return CreatedAtAction(nameof(CreatePromotionalMedia), response);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}