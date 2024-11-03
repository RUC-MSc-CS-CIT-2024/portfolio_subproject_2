namespace CitMovie.Api;

[ApiController]
[Route("api/title_types")]
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
    public async Task<IActionResult> GetTitleTypes(int page = 0, int pageSize = 10)
    {
        var titleTypes = await _titleTypeManager.GetTytleTypesAsync(page, pageSize);
        var total_items = await _titleTypeManager.GetTotalTitleTypeCountAsync();

        var result = _pagingHelper.CreatePaging(nameof(GetTitleTypes), page, pageSize, total_items, titleTypes);

        return Ok(result);
    }
}