using CitMovie.Models.Dto;

namespace CitMovie.Business.Managers;

public interface IJobCategoryManager
{
    Task<IList<JobCategoryDto>> GetAllJobCategoriesAsync(int page, int pageSize);
    Task<int> GetTotalJobCategoriesCountAsync();
}
