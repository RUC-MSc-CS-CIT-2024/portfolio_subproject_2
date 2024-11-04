namespace CitMovie.Data;

public interface ITitleTypeRepository
{
    Task<List<TitleType>> GetTitleTypesAsync(int page, int pageSize);
    Task<TitleType> GetTitleTypeByIdAsync(int TitleTypeId);
    Task<List<TitleType>> GetMultipleByIdsAsync(IEnumerable<int> ids);
    Task<int> GetTotalTitleTypesCountAsync();
}