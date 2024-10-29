using System;
using CitMovie.Data.Repositories;
using CitMovie.Models.DTOs;

namespace CitMovie.Business.Managers;

public class CountryManager
{
    private readonly ICountryRepository _countryRepository;


    public CountryManager(ICountryRepository countryRepository)
    {
        _countryRepository = countryRepository;
    }

    public async Task<IEnumerable<CountryDto>> GetAllCountriesAsync(int page, int pageSize)
    {
        return await _countryRepository.GetAllCountriesAsync(page, pageSize);
    }

    public async Task<int> GetTotalCountriesCountAsync()
    {
        return await _countryRepository.GetTotalCountriesCountAsync();
    }
}
