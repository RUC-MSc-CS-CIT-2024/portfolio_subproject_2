using CitMovie.Models.DomainObjects;

namespace CitMovie.Data;

public interface ILanguageRepository
{
    Task<Language?> GetLanguageByIdAsync(int languageId);
    Task<IList<Language>> GetLanguagesAsync();
    Task <Language> CreateLanguageAsync(Language language);
    Task <Language> UpdateLanguageAsync(Language language);
    Task <Language>DeleteLanguageAsync(int languageId);
}