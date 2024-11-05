namespace CitMovie.Data;

public class SearchHistoryRepository : ISearchHistoryRepository
{
    private readonly FrameworkContext _context;

    public SearchHistoryRepository(FrameworkContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<SearchHistory>> GetUserSearchHistoriesAsync(int userId, int page, int pageSize)
    {
        return await _context.SearchHistories
            .AsNoTracking()
            .Where(sh => sh.UserId == userId)
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> GetUsersTotalSearchHistoriesCountAsync(int userId)
    {
        return await _context.SearchHistories
            .Where(sh => sh.UserId == userId)
            .CountAsync();

    }

    public async Task<bool> DeleteUsersSearchHistoriesAsync(int userId, int searchHistoryId)
    {
        var searchHistory = await _context.SearchHistories
            .FirstOrDefaultAsync(x => x.SearchHistoryId == searchHistoryId && x.UserId == userId);

        if (searchHistory != null)
        {
            _context.SearchHistories.Remove(searchHistory);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;

    }
}
