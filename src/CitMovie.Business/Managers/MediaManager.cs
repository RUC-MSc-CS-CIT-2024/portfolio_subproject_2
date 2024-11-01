using System.Text.Json;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CitMovie.Business;

public class MediaManager : IMediaManager {
    private readonly IMediaRepository _mediaRepository;
    private readonly IMapper _mapper;

    public MediaManager(IMediaRepository mediaRepository, IMapper mapper)
    {
        _mediaRepository = mediaRepository;
        _mapper = mapper;
    }

    
    public IEnumerable<Media> GetAllMedia(PageQueryParameter page)
    {
        return _mediaRepository.GetAllMedia(page.Number, page.Number);
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
