namespace CitMovie.Data;

public interface ITitleAttributeRepository
{
    Task<IEnumerable<TitleAttribute>> GetTitleAttributesAsync(int page, int pageSize);
    Task<TitleAttribute> GetTitleAttributeByIdAsync(int id);
    Task<int> GetTotalTitleAttributesCountAsync();
}