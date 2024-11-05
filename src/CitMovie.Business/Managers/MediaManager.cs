
namespace CitMovie.Business;

public class MediaManager : IMediaManager {
    private readonly IMediaRepository _mediaRepository;
    private readonly ICrewRepository _crewRepository;
    private readonly IMapper _mapper;

    public MediaManager(
        IMediaRepository mediaRepository, 
        ICrewRepository crewRepository,
        IMapper mapper)
    {
        _mediaRepository = mediaRepository;
        _crewRepository = crewRepository;
        _mapper = mapper;
    }

    public MediaResult? Get(int id)
    {
        Media? media = _mediaRepository.GetDetailed(id);
        if (media is null)
            return null;
        return _mapper.Map<MediaResult>(media);
    }

    public IEnumerable<MediaBasicResult> GetAllMedia(PageQueryParameter page)
    {
        IEnumerable<Media> result = _mediaRepository.GetAll(page.Number, page.Number);
        return _mapper.Map<IEnumerable<MediaBasicResult>>(result);
    }

    public async Task<IEnumerable<CrewResult>> GetCrew(int mediaId, PageQueryParameter page)
    {
        if (_mediaRepository.Get(mediaId) == null)
            throw new KeyNotFoundException();

        IEnumerable<CrewMember> result = await _crewRepository.GetCrew(mediaId, page.Number, page.Count);
        return _mapper.Map<IEnumerable<CrewResult>>(result);
    }

    public async Task<IEnumerable<CrewResult>> GetCast(int mediaId, PageQueryParameter page)
    {
        if (_mediaRepository.Get(mediaId) == null)
            throw new KeyNotFoundException();

        IEnumerable<CastMember> result = await _crewRepository.GetCast(mediaId, page.Number, page.Count);
        return _mapper.Map<IEnumerable<CrewResult>>(result);
    }

    public IEnumerable<MediaBasicResult> GetRelated(int id, PageQueryParameter page)
    {
        IEnumerable<Media> result = _mediaRepository.GetRelated(id, page.Number, page.Count);
        return _mapper.Map<IEnumerable<MediaBasicResult>>(result); 
    }

    public IEnumerable<MediaBasicResult> GetSimilar(int id, PageQueryParameter page)
    {
        IEnumerable<Media> result = _mediaRepository.GetSimilar(id, page.Number, page.Count);
        return _mapper.Map<IEnumerable<MediaBasicResult>>(result);
    }

    public IEnumerable<MediaBasicResult> Search(MediaQueryParameter query, int? userId)
    {
        PageQueryParameter pageQuery = query.Page;
        IEnumerable<Media> result = query.QueryType switch {
            MediaQueryType.ExactMatch => _mediaRepository.SearchExactMatch(query.Keywords ?? [], pageQuery.Number, pageQuery.Count),
            MediaQueryType.BestMatch => _mediaRepository.SearchBestMatch(query.Keywords ?? [], pageQuery.Number, pageQuery.Count),
            MediaQueryType.Simple => _mediaRepository.SearchSimple(query.Query ?? "", userId ?? -1, pageQuery.Number, pageQuery.Count),
            MediaQueryType.Structured => _mediaRepository.SearchStructured(query.Title, query.Plot, query.Character, query.PersonName, userId ?? -1, pageQuery.Number, pageQuery.Count),
            _ => []
        };
        
        return _mapper.Map<IEnumerable<MediaBasicResult>>(result);
    }
}
