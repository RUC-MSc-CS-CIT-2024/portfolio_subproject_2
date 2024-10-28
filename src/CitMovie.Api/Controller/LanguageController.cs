namespace CitMovie.Api;

[ApiController]
[Route("api/languages")]

public class LanguageController : ControllerBase
{
    private readonly LanguageManager _languageManager;
    private readonly LinkGenerator _linkGenerator;

    public LanguageController(LanguageManager languageManager, LinkGenerator linkGenerator)
    {
        _languageManager = languageManager;
        _linkGenerator = linkGenerator;
    }

    [HttpGet(Name = nameof(GetLanguages))]
    public async Task<IActionResult> GetLanguages( int page = 0, int pageSize = 10)
    {
        
        var languages = await _languageManager.GetLanguagesAsync(page, pageSize);
        var total_items = await _languageManager.GetTotalLanguageCountAsync();
        
        var result = CreatePaging(nameof(GetLanguages),page, pageSize, total_items, languages);
        
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