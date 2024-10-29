using System.Security.Cryptography.X509Certificates;

namespace CitMovie.Api;

[ApiController]
[Route("api/promotional_media")]
public class PromotionalMediaController : ControllerBase
{
    private readonly IPromotionalMediaManager _manager;
    private readonly LinkGenerator _linkGenerator;

    public PromotionalMediaController(IPromotionalMediaManager manager, LinkGenerator linkGenerator)
    {
        _manager = manager;
        _linkGenerator = linkGenerator;
    }

    [HttpGet(Name = nameof(GetPromotionalMedia))]
    public async Task<IActionResult> GetPromotionalMedia( int page = 0, int pageSize = 10)
    {
        var promotional_media = await  _manager.GetPromotionalMediaAsync(page, pageSize);
        var total_items =await  _manager.GetPromotionalMediaCountAsync();

        var result = CreatePaging(nameof(GetPromotionalMedia),page, pageSize, total_items, promotional_media);
        
        return Ok(result);
    }

    [HttpGet("{id}",Name = nameof(GetPromotionalMediaById))]
    public async Task<IActionResult> GetPromotionalMediaById(int id)
    {
        try
        {
            var promotional_media = await _manager.GetPromotionalMediaByIdAsync(id);
            var result = new
            {
                PromotionalMediaId = GetUrl(promotional_media.PromotionalMediaId),
                ReleaseId = promotional_media.ReleaseId, // Should return ReleaseController.GetReleasebyId
                Type = promotional_media.Type,
                Uri = promotional_media.Uri
            };
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest("Promotional media not found");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePromotionalMedia(int id)
    {
        try {
            bool deleted = await _manager.DeletePromotionalMediaAsync(id);
            return deleted 
                ? NoContent() 
                : NotFound("Promotional media not found");
        } catch (Exception ex) {
            return BadRequest("Delete failed");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreatePromotionalMedia([FromBody] CreatePromotionalMediaDto model)
    {
        try
        {
            var response = await _manager.CreatePromotionalMediaAsync(model);
            return CreatedAtAction(nameof(CreatePromotionalMedia), new { id = response.PromotionalMediaId }, response);
        }
        catch (Exception e)
        {
            return BadRequest("Can not create promotional media");
        }
    }
    
    private string? GetUrl(int id)
    {
        return _linkGenerator.GetUriByName(
            HttpContext,
            nameof(GetPromotionalMediaById),
            new { id }
        );
    }
    
    private string? GetLink(string linkName, int page, int pageSize)
    {
        var uri = _linkGenerator.GetUriByName(
            HttpContext,
            linkName,
            new {page, pageSize }
        );
        return uri;
    }
    
    private object CreatePaging<T>(string linkName, int page, int pageSize, int total, IEnumerable<T?> items)
    {
        var numberOfPages = (int)Math.Ceiling(total / (double)pageSize);

        var curPage = GetLink(linkName, page, pageSize);

        var nextPage = page < numberOfPages - 1
            ? GetLink(linkName, page + 1, pageSize)
            : null;

        var prevPage = page > 0
            ? GetLink(linkName, page - 1, pageSize)
            : null;

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