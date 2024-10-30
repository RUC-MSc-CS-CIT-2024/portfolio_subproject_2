using CitMovie.Models.Dto;

namespace CitMovie.Business.Managers;

public interface IJobCategoryManager
{
    Task<IEnumerable<JobCategoryDto>> GetAllJobCategoriesAsync(int page, int pageSize);
    Task<int> GetTotalJobCategoriesCountAsync();
}
