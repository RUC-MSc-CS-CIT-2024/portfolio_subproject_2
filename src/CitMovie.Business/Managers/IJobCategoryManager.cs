using CitMovie.Models.Dto;

namespace CitMovie.Business;

public interface IJobCategoryManager
{
    Task<IEnumerable<JobCategoryResult>> GetAllJobCategoriesAsync(int page, int pageSize);
    Task<int> GetTotalJobCategoriesCountAsync();
}
