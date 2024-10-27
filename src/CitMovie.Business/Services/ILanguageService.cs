using CitMovie.Models.DTOs;

namespace CitMovie.Business;

public interface ILanguageService
{ protected Task<IList<LanguageDetailsDto>> GetLanguagesAsync();
}