namespace CitMovie.Data;

public interface ILanguageRepository
{
    Task<Language?> GetLanguageByIdAsync(int languageId);
    Task<List<Language>> GetLanguagesAsync( int page, int pageSize);
    Task<List<Language>> GetLanguagesAsync(IEnumerable<int> releaseSpokenLanguages);
    Task<int> GetTotalFollowingsCountAsync();
}