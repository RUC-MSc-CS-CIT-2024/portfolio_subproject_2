namespace CitMovie.Api;

[ApiController]
[Route("api/job_categories")]
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
    public async Task<ActionResult<IEnumerable<JobCategoryResult>>> GetAllJobCategories(int page = 0, int pageSize = 10)
    {
        var totalItems = await _jobCategoryManager.GetTotalJobCategoriesCountAsync();
        var jobCategories = await _jobCategoryManager.GetAllJobCategoriesAsync(page, pageSize);

        var result = _pagingHelper.CreatePaging(nameof(GetAllJobCategories), page, pageSize, totalItems, jobCategories);

        return Ok(result);
    }
}