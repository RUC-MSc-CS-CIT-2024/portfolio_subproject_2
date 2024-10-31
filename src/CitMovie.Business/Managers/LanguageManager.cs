namespace CitMovie.Business;

public class LanguageManager : ILanguageManager
{

    private readonly ILanguageRepository _repository;
    private readonly IMapper _mapper;

    public LanguageManager(ILanguageRepository languageRepository, IMapper mapper)
    {
        _repository = languageRepository;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<LanguageResult>> GetLanguagesAsync(int pageNumber, int pageSize)
    {
        var languages = await _repository.GetLanguagesAsync(pageNumber, pageSize);
        return _mapper.Map<IEnumerable<LanguageResult>>(languages);
    }
    
    public async Task<int> GetTotalLanguageCountAsync()
    {
        return await _repository.GetTotalFollowingsCountAsync();
    }
}

