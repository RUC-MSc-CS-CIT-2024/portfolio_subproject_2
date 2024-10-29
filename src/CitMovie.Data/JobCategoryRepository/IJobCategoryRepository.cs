using CitMovie.Models.Dto;

namespace CitMovie.Data.JobCategoryRepository
{
    public interface IJobCategoryRepository
    {
        Task<IList<JobCategory>> GetAllJobCategoriesAsync(int page, int pageSize);
        Task<int> GetTotalJobCategoriesCountAsync();
    }
}