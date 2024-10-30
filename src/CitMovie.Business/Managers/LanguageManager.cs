namespace CitMovie.Business;

public class LanguageManager : ILanguageManager
{

    private readonly ILanguageRepository _repository;
    
    public LanguageManager(ILanguageRepository languageRepository)
    {
        _repository = languageRepository;
    }
    
    public async Task<IEnumerable<LanguageResult>> GetLanguagesAsync(int pageNumber, int pageSize)
    {
        var languages = await _repository.GetLanguagesAsync(pageNumber, pageSize);
        return languages.Select(l => new LanguageResult(
            l.LanguageId,
            l.Name,
            l.IsoCode
        ));
    }
    
    public async Task<int> GetTotalLanguageCountAsync()
    {
        return await _repository.GetTotalFollowingsCountAsync();
    }
}

