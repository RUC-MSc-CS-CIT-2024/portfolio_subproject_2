namespace CitMovie.Api;

[ApiController]
[Route("api/persons")]
public class PersonController : ControllerBase
{
    private readonly IPersonManager _personManager;
    private readonly LinkGenerator _linkGenerator;

    public PersonController(IPersonManager personManager, LinkGenerator linkGenerator)
    {
        _personManager = personManager;
        _linkGenerator = linkGenerator;
    }

    [HttpGet(Name = nameof(GetPersons))]
    public async Task<ActionResult<IEnumerable<PersonResult>>> GetPersons(
        [FromQuery] int page = 0,
        [FromQuery] int pageSize = 10)
    {
        var persons = await _personManager.GetPersonsAsync(page, pageSize);
        var totalCount = await _personManager.GetTotalPersonsCountAsync();

        var result = CreatePaging<PersonResult>(
            nameof(GetPersons),
            null,
            page,
            pageSize,
            totalCount,
            persons
        );

        return Ok(result);
    }

    [HttpGet("{id}", Name = nameof(GetPersonById))]
    public async Task<ActionResult<PersonResult>> GetPersonById(int id)
    {
        var person = await _personManager.GetPersonByIdAsync(id);
        if (person == null)
        {
            return NotFound();
        }

        return Ok(person);
    }


    [HttpGet("{id}/media", Name = nameof(GetMediaByPersonId))]
    public async Task<ActionResult<IEnumerable<MediaResult>>> GetMediaByPersonId(int id,
          [FromQuery] int page = 0,
          [FromQuery] int pageSize = 10)
    {
        var media = await _personManager.GetMediaByPersonIdAsync(id, page, pageSize);
        var totalCount = await _personManager.GetMediaByPersonIdCountAsync(id);

        var result = CreatePaging<MediaResult>(
            nameof(GetMediaByPersonId),
            id,
            page,
            pageSize,
            totalCount,
            media
        );

        return Ok(result);
    }


    // HATEOAS and Pagination
    private string? GetLink(string linkName, int? id, int page, int pageSize)
    {
        var uri = _linkGenerator.GetUriByName(
                    HttpContext,
                    linkName,
                    new { id, page, pageSize }
                    );
        return uri;
    }

    private object CreatePaging<T>(string linkName, int? id, int page, int pageSize, int total, IEnumerable<T?> items)
    {
        var numberOfPages = (int)Math.Ceiling(total / (double)pageSize);

        var curPage = GetLink(linkName, id, page, pageSize);

        var nextPage = page < numberOfPages - 1
            ? GetLink(linkName, id, page + 1, pageSize)
            : null;

        var prevPage = page > 0
            ? GetLink(linkName, id, page - 1, pageSize)
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

