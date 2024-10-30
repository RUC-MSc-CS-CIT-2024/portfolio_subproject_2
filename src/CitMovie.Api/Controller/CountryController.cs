namespace CitMovie.Api;

[ApiController]
[Route("api/countries")]
public class CountryController : ControllerBase
{
    private readonly ICountryManager _countryManager;
    private readonly LinkGenerator _linkGenerator;

    public CountryController(ICountryManager countryManager, LinkGenerator linkGenerator)
    {
        _countryManager = countryManager;
        _linkGenerator = linkGenerator;
    }

    [HttpGet(Name = nameof(GetAllCountries))]
    public async Task<ActionResult<IEnumerable<CountryResult>>> GetAllCountries(int page = 0, int pageSize = 10)
    {
        var totalItems = await _countryManager.GetTotalCountriesCountAsync();
        var countries = await _countryManager.GetAllCountriesAsync(page, pageSize);

        var result = CreatePaging(
            nameof(GetAllCountries),
            page,
            pageSize,
            totalItems,
            countries
        );

        return Ok(result);
    }


    // HATEOAS and Pagination
    private string? GetLink(string linkName, int page, int pageSize)
    {
        var uri = _linkGenerator.GetUriByName(
                    HttpContext,
                    linkName,
                    new { page, pageSize }
                    );
        return uri;
    }

    private object CreatePaging<T>(string linkName, int page, int pageSize, int total, IEnumerable<T?> items)
    {
        var numberOfPages = (int)Math.Ceiling(total / (double)pageSize);

        var curPage = GetLink(linkName, page, pageSize);

        var nextPage = page < numberOfPages - 1
            ? GetLink(linkName, page + 1, pageSize)
            : null;

        var prevPage = page > 0
            ? GetLink(linkName, page - 1, pageSize)
            : null;

        var result = new
        {
            CurPage = curPage,
            NextPage = nextPage,
            PrevPage = prevPage,
            NumberOfItems = total,
            NumberPages = numberOfPages,
            Items = items
        };
        return result;
    }
}
