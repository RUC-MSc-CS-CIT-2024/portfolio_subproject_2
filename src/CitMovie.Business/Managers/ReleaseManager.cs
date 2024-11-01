namespace CitMovie.Business;

public class ReleaseManager : IReleaseManager
{
    private readonly IReleaseRepository _releaseRepository;
    private readonly IMapper _mapper;

    public ReleaseManager(IReleaseRepository releaseRepository, IMapper mapper)
    {
        _releaseRepository = releaseRepository;
        _mapper = mapper;
    }

    public async Task<List<ReleaseResult>> GetReleasesOfMediaAsync(int mediaId, int page, int pageSize)
    {
        var releases = await _releaseRepository.GetReleasesOfMediaAsync(mediaId, page, pageSize);
        
        var releaseResults = _mapper.Map<List<ReleaseResult>>(releases);
        
        return releaseResults;
    }
    
    
}