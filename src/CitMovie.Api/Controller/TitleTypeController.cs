namespace CitMovie.Api;

[ApiController]
[Route("api/title_types")]
[Tags("Base data")]
public class TitleTypeController : ControllerBase
{
    private readonly ITitleTypeManager _titleTypeManager;
    private readonly PagingHelper _pagingHelper;

    public TitleTypeController(ITitleTypeManager titleTypeManager, PagingHelper pagingHelper)
    {
        _titleTypeManager = titleTypeManager;
        _pagingHelper = pagingHelper;
    }

    [HttpGet(Name = nameof(GetTitleTypes))]
    public async Task<IActionResult> GetTitleTypes([FromQuery(Name = "")] PageQueryParameter page)
    {
        var titleTypes = await _titleTypeManager.GetTytleTypesAsync(page.Number, page.Count);
        var total_items = await _titleTypeManager.GetTotalTitleTypeCountAsync();

        var result = _pagingHelper.CreatePaging(nameof(GetTitleTypes), page.Number, page.Count, total_items, titleTypes);

        return Ok(result);
    }
}