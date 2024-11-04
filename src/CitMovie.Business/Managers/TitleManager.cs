
namespace CitMovie.Business;

public class TitleManager : ITitleManager
{
    private readonly ITitleRepository _titleRepository;
    private readonly IMediaRepository _mediaRepository;
    private readonly ITitleTypeRepository _titleTypeRepository;
    private readonly ITitleAttributeRepository _titleAttributeRepository;
    private readonly IMapper _mapper;

    public TitleManager(
        ITitleRepository titleRepository, 
        IMediaRepository mediaRepository, 
        ITitleTypeRepository titleTypeRepository, 
        ITitleAttributeRepository titleAttributeRepository,
        IMapper mapper)
    {
        _titleRepository = titleRepository;
        _mediaRepository = mediaRepository;
        _titleTypeRepository = titleTypeRepository;
        _titleAttributeRepository = titleAttributeRepository;
        _mapper = mapper;
    }

    public async Task<TitleResult> CreateAsync(int mediaId, TitleCreateRequest createRequest)
    {
        Title t = _mapper.Map<Title>(createRequest);
        if (createRequest.AttributeIds is not null) {
            List<TitleAttribute> attributes = await _titleAttributeRepository.GetMultipleByIdsAsync(createRequest.AttributeIds);
            t.TitleAttributes.AddRange(attributes);
        }
        if (createRequest.TypeIds is not null) {
            List<TitleType> types = await _titleTypeRepository.GetMultipleByIdsAsync(createRequest.TypeIds);
            t.TitleTypes.AddRange(types);
        }
        t.MediaId = mediaId;

        Title newT = await _titleRepository.CreateAsync(t);
        return _mapper.Map<TitleResult>(newT);
    }

    public async Task<bool> DeleteAsync(int mediaId, int titleId)
    {
        if (_mediaRepository.Get(mediaId) is null)
            throw new InvalidOperationException();
        
        return await _titleRepository.DeleteAsync(titleId);
    }

    public TitleResult Get(int mediaId, int titleId)
    {
        if (_mediaRepository.Get(mediaId) is null)
            throw new InvalidOperationException();

        Title result = _titleRepository.Get(titleId);
        return _mapper.Map<TitleResult>(result);
    }

    public IEnumerable<TitleResult> GetAllForMedia(int mediaId, PageQueryParameter page)
    {
        IEnumerable<Title> result = _titleRepository.GetForMedia(mediaId);
        return _mapper.Map<IEnumerable<TitleResult>>(result);
    }
}