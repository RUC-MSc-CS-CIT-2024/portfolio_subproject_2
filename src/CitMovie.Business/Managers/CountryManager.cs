namespace CitMovie.Business;

public class CountryManager : ICountryManager
{
    private readonly ICountryRepository _countryRepository;


    public CountryManager(ICountryRepository countryRepository)
    {
        _countryRepository = countryRepository;
    }

    public async Task<IEnumerable<CountryResult>> GetAllCountriesAsync(int page, int pageSize)
    {
        var countries = await _countryRepository.GetAllCountriesAsync(page, pageSize);
        return countries.Select(c => new CountryResult(
            c.CountryId,
            c.ImdbCountryCode,
            c.IsoCode,
            c.Name
        )).ToList();
    }

    public async Task<int> GetTotalCountriesCountAsync()
    {
        return await _countryRepository.GetTotalCountriesCountAsync();
    }
}
