using CitMovie.Models.DTOs;

namespace CitMovie.Business.Managers;

public interface ICountryManager
{
    Task<IEnumerable<CountryDto>> GetAllCountriesAsync(int page, int pageSize);
    Task<int> GetTotalCountriesCountAsync();
}
