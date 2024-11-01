namespace CitMovie.Business;

public class CountryManager : ICountryManager
{
    private readonly ICountryRepository _countryRepository;
    private readonly IMapper _mapper;

    public CountryManager(ICountryRepository countryRepository, IMapper mapper)
    {
        _countryRepository = countryRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CountryResult>> GetAllCountriesAsync(int page, int pageSize)
    {
        var countries = await _countryRepository.GetAllCountriesAsync(page, pageSize);
        return _mapper.Map<IEnumerable<CountryResult>>(countries);
    }

    public async Task<int> GetTotalCountriesCountAsync()
    {
        return await _countryRepository.GetTotalCountriesCountAsync();
    }
}
