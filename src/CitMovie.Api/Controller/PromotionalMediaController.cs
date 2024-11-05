using CitMovie.Models;

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
    public async Task<IActionResult> GetPromotionalMediaofMedia(int mediaId, [FromQuery] PageQueryParameter page)
    {
        try
        {
            var promotionalMedia = await _manager.GetPromotionalMediaOfMediaAsync(mediaId, page.Number, page.Count);
            var totalItems = await _manager.GetPromotionalMediaCountAsync(mediaId, "media");

            var updatedItems = promotionalMedia.Select(item =>
            {
                var result = new PromotionalMediaMinimalInfoResult
                {
                    PromotionalMediaId = _pagingHelper.GetResourceLink(nameof(GetPromotionalMediaById), new { mediaId = item.MediaId, releaseId = item.ReleaseId, id = item.PromotionalMediaId }) ?? string.Empty,
                    Type = item.Type,
                    Uri = item.Uri
                };
                AddPromotionalMediaLinks(result, item.MediaId, item.ReleaseId);
                return result;
            }).ToList();

            var result = _pagingHelper.CreatePaging(
                nameof(GetPromotionalMediaofMedia),
                page.Number, page.Count,
                totalItems,
                updatedItems,
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
    public async Task<IActionResult> GetPromotionalMediaOfRelease(int mediaId, int releaseId, [FromQuery] PageQueryParameter page)
    {
        try
        {
            var promotionalMedia = await _manager.GetPromotionalMediaOfReleaseAsync(mediaId, releaseId, page.Number, page.Count);
            var totalItems = await _manager.GetPromotionalMediaCountAsync(releaseId, "release");

            var updatedItems = promotionalMedia.Select(item =>
           {
               var result = new PromotionalMediaMinimalInfoResult
               {
                   PromotionalMediaId = _pagingHelper.GetResourceLink(nameof(GetPromotionalMediaById), new { mediaId = item.MediaId, releaseId = item.ReleaseId, id = item.PromotionalMediaId }) ?? string.Empty,
                   Type = item.Type,
                   Uri = item.Uri
               };
               AddPromotionalMediaLinks(result, item.MediaId, item.ReleaseId);
               return result;
           }).ToList();

            var result = _pagingHelper.CreatePaging(
                nameof(GetPromotionalMediaOfRelease),
                page.Number, page.Count,
                totalItems,
                updatedItems,
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
    public async Task<IActionResult> GetPromotionalMediaById(int id, int? mediaId, int? releaseId)
    {

        try
        {
            var promotionalMedia = await _manager.GetPromotionalMediaByIdAsync(id, mediaId, releaseId);
            var result = new PromotionalMediaMinimalInfoResult
            {
                PromotionalMediaId = _pagingHelper.GetResourceLink(nameof(GetPromotionalMediaOfRelease), new { mediaId = promotionalMedia.MediaId, releaseId = promotionalMedia.ReleaseId, promotionalMediaId = promotionalMedia.PromotionalMediaId }) ?? string.Empty,
                Type = promotionalMedia.Type,
                Uri = promotionalMedia.Uri
            };
            AddPromotionalMediaLinks(result, promotionalMedia.MediaId, promotionalMedia.ReleaseId);
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

            var result = new PromotionalMediaMinimalInfoResult
            {
                PromotionalMediaId = _pagingHelper.GetResourceLink(nameof(GetPromotionalMediaById), new { mediaId = response.MediaId, releaseId = response.ReleaseId, promotionalMediaId = response.PromotionalMediaId }) ?? string.Empty,
                Type = response.Type,
                Uri = response.Uri
            };
            return CreatedAtAction(nameof(CreatePromotionalMedia), result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    private void AddPromotionalMediaLinks(PromotionalMediaMinimalInfoResult promotionalMedia, int mediaId, int releaseId)
    {
        promotionalMedia.Links.Add(new Link
        {
            Href = _pagingHelper.GetResourceLink(nameof(MediaController.Get), new { id = mediaId }) ?? string.Empty,
            Rel = "media",
            Method = "GET"
        });

        promotionalMedia.Links.Add(new Link
        {
            Href = _pagingHelper.GetResourceLink(nameof(ReleaseController.GetReleaseOfMediaById), new { mediaId, id = releaseId }) ?? string.Empty,
            Rel = "release",
            Method = "GET"
        });
    }
}