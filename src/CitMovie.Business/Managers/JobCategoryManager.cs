using CitMovie.Data.JobCategoryRepository;
using CitMovie.Models.Dto;

namespace CitMovie.Business
{
    public class JobCategoryManager
    {
        private readonly IJobCategoryRepository _jobCategoryRepository;
        public JobCategoryManager(IJobCategoryRepository jobCategoryRepository)
        {
            _jobCategoryRepository = jobCategoryRepository;
        }

        public async Task<IEnumerable<JobCategoryDto>> GetAllJobCategoriesAsync(int page, int pageSize)
        {
            return await _jobCategoryRepository.GetAllJobCategoriesAsync(page, pageSize);
        }

        public async Task<int> GetTotalJobCategoriesCountAsync()
        {
            return await _jobCategoryRepository.GetTotalJobCategoriesCountAsync();
        }

    }
}