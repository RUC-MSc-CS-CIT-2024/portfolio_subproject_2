namespace CitMovie.Api;

[ApiController]
[Route("api/countries")]
public class CountryController : ControllerBase
{
    private readonly ICountryManager _countryManager;
    private readonly PagingHelper _pagingHelper;


    public CountryController(ICountryManager countryManager, PagingHelper pagingHelper)
    {
        _countryManager = countryManager;
        _pagingHelper = pagingHelper;
    }

    [HttpGet(Name = nameof(GetAllCountries))]
    public async Task<ActionResult<IEnumerable<CountryResult>>> GetAllCountries(int page = 0, int pageSize = 10)
    {
        var totalItems = await _countryManager.GetTotalCountriesCountAsync();
        var countries = await _countryManager.GetAllCountriesAsync(page, pageSize);

        var result = _pagingHelper.CreatePaging(nameof(GetAllCountries), page, pageSize, totalItems, countries);

        return Ok(result);
    }
}
