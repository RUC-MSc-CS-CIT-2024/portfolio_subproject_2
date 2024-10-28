namespace CitMovie.Business;

public interface ITitleTypeManager
{
    Task<IEnumerable<TitleTypeInfoDto>> GetTytleTypesAsync(int pageNumber, int pageSize);
    
    Task<int> GetTotalTitleTypeCountAsync();
}