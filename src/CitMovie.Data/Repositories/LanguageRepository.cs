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

        public Task<Language> CreateLanguageAsync(Language language)
        {
            throw new NotImplementedException();
        }

        public Task<Language> UpdateLanguageAsync(Language language)
        {
            throw new NotImplementedException();
        }

        public Task<Language> DeleteLanguageAsync(int languageId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetTotalFollowingsCountAsync()
        {
            return await _context.Languages.CountAsync();
        }
    }
}
