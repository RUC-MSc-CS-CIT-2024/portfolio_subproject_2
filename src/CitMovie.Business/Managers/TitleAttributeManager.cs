namespace CitMovie.Business;

public class TitleAttributeManager : ITitleAttributeManager
{
    private readonly ITitleAttributeRepository _titleAttributeRepository;
    private readonly IMapper _mapper;
    
    public TitleAttributeManager(ITitleAttributeRepository titleAttributeRepository, IMapper mapper)
    {
        _titleAttributeRepository = titleAttributeRepository;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<TitleAttributeResult>> GetTitleAttributesAsync(int page, int pageSize)
    {
        var titleAttributes = await _titleAttributeRepository.GetTitleAttributesAsync(page, pageSize);
        return _mapper.Map<IEnumerable<TitleAttributeResult>>(titleAttributes);
    }
    
    public async Task<int> GetTotalTitleAttributesCountAsync()
    {
        return await _titleAttributeRepository.GetTotalTitleAttributesCountAsync();
    }
}