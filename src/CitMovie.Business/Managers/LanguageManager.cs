using CitMovie.Models.DTOs;

namespace CitMovie.Business;

public class LanguageManager : ILanguageManager
{

    private readonly ILanguageRepository _repository;
    
    public LanguageManager(ILanguageRepository languageRepository)
    {
        _repository = languageRepository;
    }
    
    public async Task<IEnumerable<LanguageDetailsDto>> GetLanguagesAsync(int pageNumber, int pageSize)
    {
        var languages = await _repository.GetLanguagesAsync(pageNumber, pageSize);
        return languages.Select(l => new LanguageDetailsDto
        {
            LanguageId = l.LanguageId,
            Name = l.Name,
            IsoCode = l.IsoCode
        });
    }
    
    public async Task<int> GetTotalLanguageCountAsync()
    {
        return await _repository.GetTotalFollowingsCountAsync();
    }
}

