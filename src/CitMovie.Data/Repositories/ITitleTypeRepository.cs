namespace CitMovie.Data;

public interface ITitleTypeRepository
{
    Task<List<TitleType>> GetTitleTypesAsync(int page, int pageSize);
    Task<TitleType> GetTitleTypeByIdAsync(int TitleTypeId);
    Task<TitleType> AddTitleTypeAsync(TitleType titleType);
    Task<TitleType> UpdateTitleTypeAsync(TitleType titleType);
    Task<TitleType> DeleteTitleTypeAsync(TitleType titleType);
    Task<int> GetTotalTitleTypesCountAsync();
}