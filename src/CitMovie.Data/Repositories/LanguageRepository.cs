using Microsoft.EntityFrameworkCore;

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
            .Skip(pageSize * page)
            .Take(pageSize)
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

