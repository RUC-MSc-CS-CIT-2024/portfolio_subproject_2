using CitMovie.Models.DTOs;

namespace CitMovie.Business;

public interface ICountryManager
{
    Task<IEnumerable<CountryResult>> GetAllCountriesAsync(int page, int pageSize);
    Task<int> GetTotalCountriesCountAsync();
}
