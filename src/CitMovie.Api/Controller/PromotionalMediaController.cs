namespace CitMovie.Api;

[ApiController]
[Route("api/media/{mediaId}/releases/{releaseId}/promotional_media")]
public class PromotionalMediaController : ControllerBase
{
    private readonly IPromotionalMediaManager _manager;
    private readonly LinkGenerator _linkGenerator;

    public PromotionalMediaController(IPromotionalMediaManager manager, LinkGenerator linkGenerator)
    {
        _manager = manager;
        _linkGenerator = linkGenerator;
    }

    [HttpGet("/api/media/{mediaId}/promotional_media", Name = nameof(GetPromotionalMediaofMedia))]
    public async Task<IActionResult> GetPromotionalMediaofMedia(int mediaId, int page = 0, int pageSize = 10)
    {
        try
        {
            var promotionalMedia = await _manager.GetPromotionalMediaOfMediaAsync(mediaId, page, pageSize);
            var totalItems = await _manager.GetPromotionalMediaCountAsync(mediaId, "media");

            var updatedItems = promotionalMedia.Select(item => new PromotionalMediaMinimalInfoResult()
            {
                PromotionalMediaId = GetUrl(item.MediaId, item.ReleaseId, item.PromotionalMediaId),
                Type = item.Type,
                Uri = item.Uri
            });

            var result = CreatePaging(
                nameof(GetPromotionalMediaofMedia),
                page, 
                pageSize, 
                totalItems,
                updatedItems,
                mediaId);

            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet(Name = nameof(GetPromotionalMediaOfRelease))]
    public async Task<IActionResult> GetPromotionalMediaOfRelease(int mediaId, int releaseId, int page = 0, int pageSize = 10)
    {
        try
        {
            var promotionalMedia = await  _manager.GetPromotionalMediaOfReleaseAsync(mediaId, releaseId, page, pageSize);
            var totalItems =await  _manager.GetPromotionalMediaCountAsync(releaseId, "release");
        
            var updatedItems = promotionalMedia.Select(item => new PromotionalMediaMinimalInfoResult
            {
                PromotionalMediaId = GetUrl(item.MediaId,item.ReleaseId,item.PromotionalMediaId),
                Type = item.Type,
                Uri = item.Uri
            }).ToList();

            var result = CreatePaging(
                nameof(GetPromotionalMediaOfRelease),
                page, 
                pageSize, 
                totalItems,
                updatedItems,
                mediaId,
                releaseId
                );
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

    }

    [HttpGet("{id}",Name = nameof(GetPromotionalMediaById))]
    [HttpGet("/promotional_media/{id}")]
    public async Task<IActionResult> GetPromotionalMediaById( int id, int? mediaId, int? releaseId)
    {

        try
        {
            var promotionalMedia = await _manager.GetPromotionalMediaByIdAsync(id, mediaId, releaseId);
            var result = new PromotionalMediaMinimalInfoResult
            {
                PromotionalMediaId = GetUrl(promotionalMedia.MediaId, promotionalMedia.ReleaseId,promotionalMedia.PromotionalMediaId),
                Type = promotionalMedia.Type,
                Uri = promotionalMedia.Uri
            };
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
        try {
            bool deleted = await _manager.DeletePromotionalMediaAsync(mediaId, releaseId, id);
            return deleted 
                ? NoContent() 
                : NotFound("Promotional media not found");
        } catch (Exception ex) {
            return BadRequest("Delete failed");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreatePromotionalMedia([FromRoute]int mediaId, [FromRoute]int releaseId, [FromBody] PromotionalMediaCreateRequest model)
    {
        try
        {
            var response = await _manager.CreatePromotionalMediaAsync(mediaId, releaseId, model);

            var result = new PromotionalMediaMinimalInfoResult
            {
                PromotionalMediaId = GetUrl(response.MediaId, response.ReleaseId, response.PromotionalMediaId),
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
    
    private string? GetUrl(int mediaId, int releaseId, int id)
    {
        return _linkGenerator.GetUriByName(
            HttpContext,
            nameof(GetPromotionalMediaById),
            new {mediaId, releaseId, id }
        );
    }
    
    private string? GetLink(string linkName, int page, int pageSize, int mediaId)
    {
        var uriWithoutRelease = _linkGenerator.GetUriByName(
            HttpContext,
            linkName,
            new {mediaId, page, pageSize }
        );
        return uriWithoutRelease;
    }
    
    private string? GetLink(string linkName, int page, int pageSize, int? releaseId, int mediaId)
    {
        var uriWithRelease = _linkGenerator.GetUriByName(
            HttpContext,
            linkName,
            new {mediaId, releaseId, page, pageSize }
        );
        return uriWithRelease;
    }
    
    private object CreatePaging<T>( string linkName, int page, int pageSize, int total, IEnumerable<T?> items, int mediaId, int? releaseid)
    {
        var numberOfPages = (int)Math.Ceiling(total / (double)pageSize);

        var curPage = GetLink(linkName, page, pageSize, releaseid, mediaId);

        var nextPage = page < numberOfPages - 1 ? GetLink(linkName, page + 1, pageSize, releaseid, mediaId) : null;

        var prevPage = page > 0 ? GetLink(linkName, page - 1, pageSize, releaseid, mediaId) : null;

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
    
    private object CreatePaging<T>( string linkName, int page, int pageSize, int total, IEnumerable<T?> items, int mediaId)
    {
        var numberOfPages = (int)Math.Ceiling(total / (double)pageSize);

        var curPage = GetLink(linkName, page, pageSize, mediaId);

        var nextPage = page < numberOfPages - 1 ? GetLink(linkName, page + 1, pageSize, mediaId) : null;

        var prevPage = page > 0 ? GetLink(linkName, page - 1, pageSize, mediaId) : null;

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