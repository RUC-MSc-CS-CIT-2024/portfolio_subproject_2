namespace CitMovie.Business;

public interface ITitleTypeManager
{
    Task<IEnumerable<TitleTypeResult>> GetTytleTypesAsync(int pageNumber, int pageSize);
    
    Task<int> GetTotalTitleTypeCountAsync();
}