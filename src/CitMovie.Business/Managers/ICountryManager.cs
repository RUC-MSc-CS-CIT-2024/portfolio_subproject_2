using CitMovie.Models.DTOs;

namespace CitMovie.Business.Managers;

public interface ICountryManager
{
    Task<IList<CountryDto>> GetAllCountriesAsync(int page, int pageSize);
    Task<int> GetTotalCountriesCountAsync();
}
