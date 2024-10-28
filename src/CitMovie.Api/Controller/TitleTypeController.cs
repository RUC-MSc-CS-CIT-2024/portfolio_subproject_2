namespace CitMovie.Api;

[ApiController]
[Route("api/title_types")]
public class TitleTypeController : ControllerBase
{
    private readonly ITitleTypeManager _titleTypeManager;
    private readonly LinkGenerator _linkGenerator;

    public TitleTypeController(ITitleTypeManager titleTypeManager, LinkGenerator linkGenerator)
    {
        _titleTypeManager = titleTypeManager;
        _linkGenerator = linkGenerator;
    }

    [HttpGet(Name = nameof(GetTitleTypes))]
    public async Task<IActionResult> GetTitleTypes(int page = 0, int pageSize = 10)
    {
        var titleTypes = await _titleTypeManager.GetTytleTypesAsync(page, pageSize);
        var total_items = await _titleTypeManager.GetTotalTitleTypeCountAsync();
        
        var result = CreatePaging(nameof(GetTitleTypes), page, pageSize, total_items, titleTypes);
        
        return Ok(result);
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