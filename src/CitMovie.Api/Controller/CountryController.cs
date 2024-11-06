namespace CitMovie.Api;

[ApiController]
[Route("api/countries")]
[Tags("Base data")]
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
    public async Task<ActionResult<IEnumerable<CountryResult>>> GetAllCountries([FromQuery] PageQueryParameter page)
    {
        var totalItems = await _countryManager.GetTotalCountriesCountAsync();
        var countries = await _countryManager.GetAllCountriesAsync(page.Number, page.Count);

        var result = _pagingHelper.CreatePaging(nameof(GetAllCountries), page.Number, page.Count, totalItems, countries);

        return Ok(result);
    }
}
