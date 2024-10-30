using CitMovie.Business.Managers;
using CitMovie.Data.JobCategoryRepository;
using CitMovie.Models.Dto;

namespace CitMovie.Business
{
    public class JobCategoryManager : IJobCategoryManager
    {
        private readonly IJobCategoryRepository _jobCategoryRepository;
        public JobCategoryManager(IJobCategoryRepository jobCategoryRepository)
        {
            _jobCategoryRepository = jobCategoryRepository;
        }

        public async Task<IEnumerable<JobCategoryDto>> GetAllJobCategoriesAsync(int page, int pageSize)
        {
            var jobCategories = await _jobCategoryRepository.GetAllJobCategoriesAsync(page, pageSize);
            return jobCategories.Select(jc => new JobCategoryDto
            {
                JobCategoryId = jc.JobCategoryId,
                Name = jc.Name
            }).ToList();
        }

        public async Task<int> GetTotalJobCategoriesCountAsync()
        {
            return await _jobCategoryRepository.GetTotalJobCategoriesCountAsync();
        }

    }
}