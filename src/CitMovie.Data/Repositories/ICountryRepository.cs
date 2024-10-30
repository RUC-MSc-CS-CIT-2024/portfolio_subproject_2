namespace CitMovie.Data.Repositories;

public interface ICountryRepository
{
    Task<IEnumerable<Country>> GetAllCountriesAsync(int page, int pageSize);
    Task<int> GetTotalCountriesCountAsync();

}
