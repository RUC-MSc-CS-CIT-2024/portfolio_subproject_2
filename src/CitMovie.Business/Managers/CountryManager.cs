using CitMovie.Data.Repositories;
using CitMovie.Models.DTOs;

namespace CitMovie.Business.Managers;

public class CountryManager : ICountryManager
{
    private readonly ICountryRepository _countryRepository;


    public CountryManager(ICountryRepository countryRepository)
    {
        _countryRepository = countryRepository;
    }

    public async Task<IEnumerable<CountryDto>> GetAllCountriesAsync(int page, int pageSize)
    {
        var countries = await _countryRepository.GetAllCountriesAsync(page, pageSize);
        return countries.Select(c => new CountryDto
        {
            CountryId = c.CountryId,
            ImdbCountryCode = c.ImdbCountryCode,
            IsoCode = c.IsoCode,
            Name = c.Name
        }).ToList();
    }

    public async Task<int> GetTotalCountriesCountAsync()
    {
        return await _countryRepository.GetTotalCountriesCountAsync();
    }
}
