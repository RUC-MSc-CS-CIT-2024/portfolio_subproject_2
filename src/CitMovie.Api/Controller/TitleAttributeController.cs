namespace CitMovie.Api;

[ApiController]
[Route("api/title_attributes")]
[Tags("Base data")]
public class TitleAttributeController : ControllerBase
{
    private readonly ITitleAttributeManager _titleAttributeManager;
    private readonly PagingHelper _pagingHelper;
    
    public TitleAttributeController(ITitleAttributeManager titleAttributeManager, PagingHelper pagingHelper)
    {
        _titleAttributeManager = titleAttributeManager;
        _pagingHelper = pagingHelper;
    }
    
    [HttpGet(Name = nameof(GetTitleAttributesAsync))]
    public async Task<IActionResult> GetTitleAttributesAsync([FromQuery(Name = "")] PageQueryParameter page)
    {
        var list = await _titleAttributeManager.GetTitleAttributesAsync(page.Number, page.Count);
        var totalItems = await _titleAttributeManager.GetTotalTitleAttributesCountAsync();
        
        var result = _pagingHelper.CreatePaging(nameof(GetTitleAttributesAsync), page.Number, page.Count, totalItems, list);
        
        return Ok(result);
    }
}