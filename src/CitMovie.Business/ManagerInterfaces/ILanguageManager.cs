namespace CitMovie.Business;

public interface ILanguageManager
{ 
    Task<IEnumerable<LanguageResult>> GetLanguagesAsync(int pageNumber, int pageSize);

    Task<int> GetTotalLanguageCountAsync();
}