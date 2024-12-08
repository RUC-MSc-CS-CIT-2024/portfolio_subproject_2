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
    public async Task<ActionResult> GetAllCountries([FromQuery] int page, [FromQuery] int count)
    {
        Console.WriteLine("Page: " + page + " Count: " + count);
        var totalItems = await _countryManager.GetTotalCountriesCountAsync();
        var countries = await _countryManager.GetAllCountriesAsync(page, count);

        var result = _pagingHelper.CreatePaging(nameof(GetAllCountries), page, count, totalItems, countries);

        return Ok(result);
    }
}
