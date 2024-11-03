namespace CitMovie.Data;

public class CompletedRepository : ICompletedRepository
{
    private readonly FrameworkContext _context;

    public CompletedRepository(FrameworkContext context) =>
        _context = context;

    public async Task<Completed> AddCompletedAsync(Completed completed)
    {
        _context.Completed.Add(completed);
        await _context.SaveChangesAsync();
        return completed;
    }

    public async Task<Completed?> GetCompletedByIdAsync(int completedId) =>
        await _context.Completed.FindAsync(completedId);

    public async Task<IEnumerable<Completed>> GetUserCompletedItemsAsync(int userId, int page, int pageSize) =>
        await _context.Completed
            .Where(c => c.UserId == userId)
            .AsNoTracking()
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync();

    public async Task<Completed> UpdateCompletedAsync(Completed completed)
    {
        _context.Completed.Update(completed);
        await _context.SaveChangesAsync();
        return completed;
    }

    public async Task<bool> DeleteCompletedAsync(int completedId)
    {
        var completed = await _context.Completed.FindAsync(completedId);
        if (completed == null) return false;

        _context.Completed.Remove(completed);
        await _context.SaveChangesAsync();
        return true;
    }
}
