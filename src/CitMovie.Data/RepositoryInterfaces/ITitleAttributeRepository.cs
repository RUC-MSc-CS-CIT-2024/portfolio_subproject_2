namespace CitMovie.Data;

public interface ITitleAttributeRepository
{
    Task<IEnumerable<TitleAttribute>> GetTitleAttributesAsync(int page, int pageSize);
    Task<TitleAttribute> GetTitleAttributeByIdAsync(int id);
    Task<List<TitleAttribute>> GetMultipleByIdsAsync(IEnumerable<int> ids);
    Task<int> GetTotalTitleAttributesCountAsync();
}