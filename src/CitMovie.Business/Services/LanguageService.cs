using CitMovie.Models.DTOs;

namespace CitMovie.Business;

public class LanguageService : ILanguageService
{

    private readonly ILanguageRepository _repository;
    
    public LanguageService(ILanguageRepository languageRepository)
    {
        _repository = languageRepository;
    }
    
    public async Task<IList<LanguageDetailsDto>> GetLanguagesAsync()
    {
        var languages = await _repository.GetLanguagesAsync();
        var languageDetailsDtos = new List<LanguageDetailsDto>();

        for (int i = 0; i < languages.Count(); i++)
        {
            languageDetailsDtos[i].LanguageId = languages[i].LanguageId;
            languageDetailsDtos[i].Name = languages[i].Name;
            languageDetailsDtos[i].IsoCode = languages[i].IsoCode;
        }
        
        return languageDetailsDtos;
    }
    
}

