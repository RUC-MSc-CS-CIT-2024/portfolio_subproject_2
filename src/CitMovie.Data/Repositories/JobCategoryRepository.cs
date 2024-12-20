namespace CitMovie.Data;
public class JobCategoryRepository : IJobCategoryRepository
{
    private readonly DataContext _context;

    public JobCategoryRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<JobCategory>> GetAllJobCategoriesAsync(int page, int pageSize)
    {
        return await _context.JobCategories
            .AsNoTracking()
            .OrderBy(jc => jc.Name)
            .ThenBy(jc => jc.JobCategoryId)
            .Pagination(page, pageSize)
            .ToListAsync();
    }

    public async Task<int> GetTotalJobCategoriesCountAsync()
    {
        return await _context.JobCategories.CountAsync();
    }
}