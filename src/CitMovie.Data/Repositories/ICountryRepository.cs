using System;
using CitMovie.Models.DTOs;

namespace CitMovie.Data.Repositories;

public interface ICountryRepository
{
    Task<IList<CountryDto>> GetAllCountriesAsync(int page, int pageSize);
    Task<int> GetTotalCountriesCountAsync();

}
