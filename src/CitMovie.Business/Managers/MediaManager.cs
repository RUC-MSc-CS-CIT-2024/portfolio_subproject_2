using System.Text.Json;

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

    public IEnumerable<MediaBasicResult> SearchExactMatch(string[] keywords, PageQueryParameter page)
    {
        IEnumerable<Media> result = _mediaRepository.SearchExactMatch(keywords, page.Number, page.Count);
        Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions() {
            ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles}));
        return _mapper.Map<IEnumerable<MediaBasicResult>>(result);
    }
}
