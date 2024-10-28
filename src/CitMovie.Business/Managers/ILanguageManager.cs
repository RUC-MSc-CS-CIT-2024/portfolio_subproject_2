using CitMovie.Models.DTOs;

namespace CitMovie.Business;

public interface ILanguageManager
{ 
    Task<IEnumerable<LanguageDetailsDto>> GetLanguagesAsync(int pageNumber, int pageSize);

    Task<int> GetTotalLanguageCountAsync();
}