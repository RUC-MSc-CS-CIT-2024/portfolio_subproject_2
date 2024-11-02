namespace CitMovie.Business;

public class JobCategoryManager : IJobCategoryManager
{
    private readonly IJobCategoryRepository _jobCategoryRepository;
    private readonly IMapper _mapper;

    public JobCategoryManager(IJobCategoryRepository jobCategoryRepository, IMapper mapper)
    {
        _jobCategoryRepository = jobCategoryRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<JobCategoryResult>> GetAllJobCategoriesAsync(int page, int pageSize)
    {
        var jobCategories = await _jobCategoryRepository.GetAllJobCategoriesAsync(page, pageSize);
        return _mapper.Map<IEnumerable<JobCategoryResult>>(jobCategories);
    }

    public async Task<int> GetTotalJobCategoriesCountAsync()
    {
        return await _jobCategoryRepository.GetTotalJobCategoriesCountAsync();
    }

}
