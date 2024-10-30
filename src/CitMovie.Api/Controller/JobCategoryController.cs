namespace CitMovie.Api;

[ApiController]
[Route("api/job_categories")]
public class JobCategoryController : ControllerBase
{
    private readonly IJobCategoryManager _jobCategoryManager;
    private readonly LinkGenerator _linkGenerator;

    public JobCategoryController(IJobCategoryManager jobCategoryManager, LinkGenerator linkGenerator)
    {
        _jobCategoryManager = jobCategoryManager;
        _linkGenerator = linkGenerator;
    }

    [HttpGet(Name = nameof(GetAllJobCategories))]
    public async Task<ActionResult<IEnumerable<JobCategoryResult>>> GetAllJobCategories(int page = 0, int pageSize = 10)
    {
        var totalItems = await _jobCategoryManager.GetTotalJobCategoriesCountAsync();
        var jobCategories = await _jobCategoryManager.GetAllJobCategoriesAsync(page, pageSize);

        var result = CreatePaging(
            nameof(GetAllJobCategories),
            page,
            pageSize,
            totalItems,
            jobCategories
        );

        return Ok(result);
    }


    // HATEOAS and Pagination
    private string? GetLink(string linkName, int page, int pageSize)
    {
        var uri = _linkGenerator.GetUriByName(
                    HttpContext,
                    linkName,
                    new { page, pageSize }
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