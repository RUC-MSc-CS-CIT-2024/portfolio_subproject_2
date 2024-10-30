using CitMovie.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace CitMovie.Data.JobCategoryRepository
{
    public class JobCategoryRepository : IJobCategoryRepository
    {
        private readonly DataContext _context;

        public JobCategoryRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IList<JobCategory>> GetAllJobCategoriesAsync(int page, int pageSize)
        {
            return await _context.JobCategories
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalJobCategoriesCountAsync()
        {
            return await _context.JobCategories.CountAsync();
        }
    }
}