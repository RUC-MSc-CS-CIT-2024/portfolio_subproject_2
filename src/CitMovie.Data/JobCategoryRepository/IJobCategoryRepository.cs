namespace CitMovie.Data.JobCategoryRepository
{
    public interface IJobCategoryRepository
    {
        Task<IEnumerable<JobCategory>> GetAllJobCategoriesAsync(int page, int pageSize);
        Task<int> GetTotalJobCategoriesCountAsync();
    }
}