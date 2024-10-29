namespace CitMovie.Data.Repositories;

public interface ICountryRepository
{
    Task<IList<Country>> GetAllCountriesAsync(int page, int pageSize);
    Task<int> GetTotalCountriesCountAsync();

}
