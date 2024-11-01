namespace CitMovie.Business;

public class TitleTypeManager : ITitleTypeManager
{
    private readonly ITitleTypeRepository _repository;
    private readonly IMapper _mapper;

    public TitleTypeManager(ITitleTypeRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<TitleTypeResult>> GetTytleTypesAsync(int pageNumber, int pageSize)
    {
        IEnumerable<TitleType> titleTypes = await _repository.GetTitleTypesAsync(pageNumber, pageSize);
        return _mapper.Map<IEnumerable<TitleTypeResult>>(titleTypes);
    }

    public async Task<int> GetTotalTitleTypeCountAsync()
    {
        return await _repository.GetTotalTitleTypesCountAsync();
    }
}