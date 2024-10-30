namespace CitMovie.Business;

public class TitleTypeManager : ITitleTypeManager
{
    private readonly ITitleTypeRepository _repository;

    public TitleTypeManager(ITitleTypeRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<IEnumerable<TitleTypeResult>> GetTytleTypesAsync(int pageNumber, int pageSize)
    {
        var titleTypes = await _repository.GetTitleTypesAsync(pageNumber, pageSize);
        return titleTypes.Select(t => new TitleTypeResult(t.TitleTypeId, t.Name));
    }

    public async Task<int> GetTotalTitleTypeCountAsync()
    {
        return await _repository.GetTotalTitleTypesCountAsync();
    }
}