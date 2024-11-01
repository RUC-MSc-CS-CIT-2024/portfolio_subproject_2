namespace CitMovie.Business;

public class ReleaseManager : IReleaseManager
{
    private readonly IReleaseRepository _releaseRepository;
    private readonly ILanguageRepository _languageRepository;
    private readonly IMapper _mapper;

    public ReleaseManager(
        IReleaseRepository releaseRepository,
        IMapper mapper,
        ILanguageRepository languageRepository)
    {
        _releaseRepository = releaseRepository;
        _languageRepository = languageRepository;
        _mapper = mapper;
    }

    public async Task<List<ReleaseResult>> GetReleasesOfMediaAsync(int mediaId, int page, int pageSize)
    {
        var data = await _releaseRepository.GetReleasesOfMediaAsync(mediaId, page, pageSize);
        
        var releaseResults = _mapper.Map<List<ReleaseResult>>(data);
        
        return releaseResults;
    }

    public async Task<ReleaseResult> GetReleaseOfMediaByIdAsync(int mediaId, int releaseId)
    {
        var data = await _releaseRepository.GetReleaseOfMediaByIdAsync(mediaId, releaseId);
        var releaseResults = _mapper.Map<ReleaseResult>(data);
        
        return releaseResults;
    }

    public async Task<bool> DeleteReleaseOfMediaAsync(int mediaId, int releaseId)
    {
        return await _releaseRepository.DeleteReleaseOfMediaAsync(mediaId, releaseId);
    }

    public async Task<ReleaseResult> CreateReleaseForMediaAsync(int mediaId, ReleaseCreateRequest release)
    {
        var createObject = _mapper.Map<Release>(release);
        var resultObject = await _releaseRepository.CreateReleaseForMediaAsync(mediaId, createObject);
        return _mapper.Map<ReleaseResult>(resultObject);
    }

    public async Task<ReleaseResult> UpdateReleaseForMediaAsync(int releaseId, ReleaseUpdateRequest release)
    {
        var createObject = _mapper.Map<Release>(release);
        var resultObject = await _releaseRepository.UpdateReleaseForMediaAsync(releaseId, createObject);
        return _mapper.Map<ReleaseResult>(resultObject);
    }

    public async Task<int> GetReleasesCountAsync(int mediaId)
    {
        return await _releaseRepository.GetReleasesCountAsync(mediaId);
    }


}