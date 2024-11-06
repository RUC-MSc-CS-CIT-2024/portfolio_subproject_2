namespace CitMovie.Api;

[ApiController]
[Route("api/job_categories")]
[Tags("Base data")]
public class JobCategoryController : ControllerBase
{
    private readonly IJobCategoryManager _jobCategoryManager;
    private readonly PagingHelper _pagingHelper;

    public JobCategoryController(IJobCategoryManager jobCategoryManager, PagingHelper pagingHelper)
    {
        _jobCategoryManager = jobCategoryManager;
        _pagingHelper = pagingHelper;
    }

    [HttpGet(Name = nameof(GetAllJobCategories))]
    public async Task<ActionResult<IEnumerable<JobCategoryResult>>> GetAllJobCategories([FromQuery] PageQueryParameter page)
    {
        var totalItems = await _jobCategoryManager.GetTotalJobCategoriesCountAsync();
        var jobCategories = await _jobCategoryManager.GetAllJobCategoriesAsync(page.Number, page.Count);

        var result = _pagingHelper.CreatePaging(nameof(GetAllJobCategories), page.Number, page.Count, totalItems, jobCategories);

        return Ok(result);
    }
}