namespace CitMovie.Business;

public interface ITitleTypeManager
{
    Task<IEnumerable<TitleTypeInfoResult>> GetTytleTypesAsync(int pageNumber, int pageSize);
    
    Task<int> GetTotalTitleTypeCountAsync();
}