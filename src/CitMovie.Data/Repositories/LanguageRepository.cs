namespace CitMovie.Data;

public class LanguageRepository : ILanguageRepository
{
    private readonly DataContext _context;

    public LanguageRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<Language?> GetLanguageByIdAsync(int languageId)
    {
        return await _context.Languages
            .FirstAsync(l => l.LanguageId == languageId);
    }

    public async Task<List<Language>> GetLanguagesAsync(int page, int pageSize)
    {
        return await _context.Languages
            .AsNoTracking()
            .OrderBy(l => l.Name)
            .ThenBy(l => l.LanguageId)
            .Pagination(page, pageSize)
            .ToListAsync();
    }
    
    public async Task<List<Language>> GetLanguagesAsync(IEnumerable<int> languageIds)
    {
        return await _context.Languages
            .Where(l => languageIds.Contains(l.LanguageId))
            .ToListAsync();
    }
    
    public async Task<int> GetTotalFollowingsCountAsync()
    {
        return await _context.Languages.CountAsync();
    }
    

    
}

