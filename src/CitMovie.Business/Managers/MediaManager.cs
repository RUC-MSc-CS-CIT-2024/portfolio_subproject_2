using System.Text.Json;

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

    public MediaResult Get(int id)
    {
        Media? media = _mediaRepository.GetDetailed(id);
        if (media is null)
            throw new KeyNotFoundException();
        
        if (media is Episode e)
            return _mapper.Map<MediaResult>(e);
        else if (media is Season s)
            return _mapper.Map<MediaResult>(s);
        else
            return _mapper.Map<MediaResult>(media);
    }

    public IEnumerable<MediaBasicResult> GetAllMedia(PageQueryParameter page)
    {
        IEnumerable<Media> result = _mediaRepository.GetAll(page.Number, page.Count);
        return _mapper.Map<IEnumerable<MediaBasicResult>>(result);
    }

    public async Task<IEnumerable<CrewResult>> GetCrewAsync(int mediaId, PageQueryParameter page)
    {
        if (_mediaRepository.Get(mediaId) == null)
            throw new KeyNotFoundException();

        IEnumerable<CrewMember> result = await _crewRepository.GetCrewAsync(mediaId, page.Number, page.Count);

        return _mapper.Map<IEnumerable<CrewResult>>(result);
    }

    public async Task<IEnumerable<CrewResult>> GetCastAsync(int mediaId, PageQueryParameter page)
    {
        if (_mediaRepository.Get(mediaId) == null)
            throw new KeyNotFoundException();

        IEnumerable<CastMember> result = await _crewRepository.GetCastAsync(mediaId, page.Number, page.Count);
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

        IEnumerable<MediaBasicResult> basicMedia = _mapper.Map<IEnumerable<MediaBasicResult>>(result.OfType<Media>());
        IEnumerable<MediaBasicResult> episodes = _mapper.Map<IEnumerable<MediaBasicResult>>(result.OfType<Episode>());
        
        return basicMedia.Concat(episodes)
            .DistinctBy(x => x.Id)
            .OrderBy(x => x.Id)
            .ThenBy(x => x.Title); 
    }

    public async Task<int> GetTotalRelatedMediaCountAsync(int id)
    {
        return await _mediaRepository.GetTotalRelatedMediaCountAsync(id);
    }

    public async Task<int> GetTotalSimilarMediaCountAsync(int id)
    {
        return await _mediaRepository.GetTotalSimilarMediaCountAsync(id);
    }
    
    public int GetSearchResultsCount(MediaQueryParameter query)
    {
        return query.QueryType switch {
            MediaQueryType.ExactMatch => _mediaRepository.GetExactMatchSearchResultsCount(query.Keywords ?? []),
            MediaQueryType.BestMatch => _mediaRepository.GetBestMatchSearchResultsCount(query.Keywords ?? []),
            MediaQueryType.Simple => _mediaRepository.GetSimpleSearchResultsCount(query.Query ?? ""),
            MediaQueryType.Structured => _mediaRepository.GetStructuredSearchResultsCount(query.Title, query.Plot, query.Character, query.PersonName),
            _ => 0
        };
    }
    
    public int GetTotalMediaCount()
    {
        return _mediaRepository.GetTotalMediaCount();
    }
}
