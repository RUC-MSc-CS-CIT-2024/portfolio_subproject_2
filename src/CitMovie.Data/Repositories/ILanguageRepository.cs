namespace CitMovie.Data;

public interface ILanguageRepository
{
    Task<Language?> GetLanguageByIdAsync(int languageId);
    Task<IList<Language>> GetLanguagesAsync( int page, int pageSize);
    Task<int> GetTotalFollowingsCountAsync();
}