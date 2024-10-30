namespace CitMovie.Business;

public interface ILanguageManager
{ 
    Task<IEnumerable<LangugeResult>> GetLanguagesAsync(int pageNumber, int pageSize);

    Task<int> GetTotalLanguageCountAsync();
}