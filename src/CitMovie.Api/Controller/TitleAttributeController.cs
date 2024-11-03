namespace CitMovie.Api;

[ApiController]
[Route("api/title_attributes")]
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
    public async Task<IActionResult> GetTitleAttributesAsync(int page = 0 , int pageSize = 10)
    {
        var list = await _titleAttributeManager.GetTitleAttributesAsync(page, pageSize);
        var totalItems = await _titleAttributeManager.GetTotalTitleAttributesCountAsync();
        
        var result = _pagingHelper.CreatePaging(nameof(GetTitleAttributesAsync), page, pageSize, totalItems, list);
        
        return Ok(result);
    }
}