using Microsoft.EntityFrameworkCore;

namespace CitMovie.Data.Repositories
{
    public class CompletedRepository : ICompletedRepository
    {
        private readonly FrameworkContext _context;

        public CompletedRepository(FrameworkContext context)
        {
            _context = context;
        }

        public async Task<Completed> AddCompletedAsync(Completed completed)
        {
            _context.Completeds.Add(completed);
            await _context.SaveChangesAsync();
            return completed;
        }

        public async Task<Completed?> GetCompletedAsync(int completedId)
        {
            return await _context.Completeds
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CompletedId == completedId);
        }

        public async Task<IEnumerable<Completed>> GetUserCompletedAsync(int userId)
        {
            return await _context.Completeds
                .AsNoTracking()
                .Where(c => c.UserId == userId)
                .ToListAsync();
        }

        public async Task<bool> DeleteCompletedAsync(int completedId)
        {
            var completed = await _context.Completeds.FindAsync(completedId);
            if (completed == null) return false;

            _context.Completeds.Remove(completed);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
