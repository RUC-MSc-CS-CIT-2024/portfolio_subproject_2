using CitMovie.Models.DTOs;

namespace CitMovie.Business;

public interface ILanguageManager
{ 
    Task<IEnumerable<LanguageDetailsResult>> GetLanguagesAsync(int pageNumber, int pageSize);

    Task<int> GetTotalLanguageCountAsync();
}