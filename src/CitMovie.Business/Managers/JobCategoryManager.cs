namespace CitMovie.Business;

public class JobCategoryManager : IJobCategoryManager
{
    private readonly IJobCategoryRepository _jobCategoryRepository;
    public JobCategoryManager(IJobCategoryRepository jobCategoryRepository)
    {
        _jobCategoryRepository = jobCategoryRepository;
    }

    public async Task<IEnumerable<JobCategoryResult>> GetAllJobCategoriesAsync(int page, int pageSize)
    {
        var jobCategories = await _jobCategoryRepository.GetAllJobCategoriesAsync(page, pageSize);
        return jobCategories.Select(jc => new JobCategoryResult(jc.JobCategoryId, jc.Name)).ToList();
    }

    public async Task<int> GetTotalJobCategoriesCountAsync()
    {
        return await _jobCategoryRepository.GetTotalJobCategoriesCountAsync();
    }

}
