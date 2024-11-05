namespace CitMovie.Api;

[ApiController]
[Route("api/languages")]
[Tags("Base data")]
public class LanguageController : ControllerBase
{
    private readonly ILanguageManager _languageManager;
    private readonly PagingHelper _pagingHelper;

    public LanguageController(ILanguageManager languageManager, PagingHelper pagingHelper)
    {
        _languageManager = languageManager;
        _pagingHelper = pagingHelper;
    }

    [HttpGet(Name = nameof(GetLanguages))]
    public async Task<IActionResult> GetLanguages(int page = 0, int pageSize = 10)
    {

        var languages = await _languageManager.GetLanguagesAsync(page, pageSize);
        var total_items = await _languageManager.GetTotalLanguageCountAsync();

        var result = _pagingHelper.CreatePaging(nameof(GetLanguages), page, pageSize, total_items, languages);

        return Ok(result);
    }
}