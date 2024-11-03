namespace CitMovie.Business;

public interface ITitleAttributeManager
{
    Task<IEnumerable<TitleAttributeResult>> GetTitleAttributesAsync(int page, int pageSize);
    Task<int> GetTotalTitleAttributesCountAsync();
}