using CitMovie.Models.DomainObjects;
using Microsoft.EntityFrameworkCore;

namespace CitMovie.Data
{
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

        public async Task<IList<Language>> GetLanguagesAsync(int page, int pageSize)
        {
            return await _context.Languages
                .Skip(pageSize * page)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalFollowingsCountAsync()
        {
            return await _context.Languages.CountAsync();
        }
    }
}
