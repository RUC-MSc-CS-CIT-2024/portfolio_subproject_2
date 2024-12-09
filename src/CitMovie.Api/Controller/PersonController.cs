namespace CitMovie.Api;

[ApiController]
[Route("api/persons")]
[Tags("Person")]
public class PersonController : ControllerBase
{
    private readonly IPersonManager _personManager;
    private readonly IMediaManager _mediaManager;
    private readonly PagingHelper _pagingHelper;

    public PersonController(IPersonManager personManager, IMediaManager mediaManager, PagingHelper pagingHelper)
    {
        _personManager = personManager;
        _mediaManager = mediaManager;
        _pagingHelper = pagingHelper;
    }

    [HttpGet(Name = nameof(GetPersons))]
    public async Task<ActionResult<IEnumerable<PersonResult>>> GetPersons([FromQuery(Name = "")] PageQueryParameter page)
    {
        var persons = await _personManager.GetPersonsAsync(page.Number, page.Count);
        var totalCount = await _personManager.GetTotalPersonsCountAsync();

        var result = _pagingHelper.CreatePaging(
            nameof(GetPersons),
            page.Number, page.Count,
            totalCount,
            persons
        );

        foreach (var person in persons)
            person.Links = AddPersonLinks(person.Id);
        

        return Ok(result);
    }

    [HttpGet("{id}", Name = nameof(GetPersonById))]
    public async Task<ActionResult<PersonResult>> GetPersonById(int id)
    {
        var person = await _personManager.GetPersonByIdAsync(id);
        if (person == null)
            return NotFound();

        person.Links = AddPersonLinks(person.Id);

        return Ok(person);
    }

    [HttpGet("{id}/media", Name = nameof(GetMediaByPersonId))]
    public async Task<ActionResult<IEnumerable<MediaBasicResult>>> GetMediaByPersonId(int id, [FromQuery(Name = "")] PageQueryParameter page)
    {
        var media = await _personManager.GetMediaByPersonIdAsync(id, page.Number, page.Count);
        var totalCount = await _personManager.GetMediaByPersonIdCountAsync(id);

        var result = _pagingHelper.CreatePaging(
            nameof(GetMediaByPersonId),
            page.Number, page.Count,
            totalCount,
            media,
            new { id }
        );

        foreach (var m in media)
            m.Links = AddMediaLinks(m);

        return Ok(result);
    }

    [HttpGet("{id}/coactors", Name = nameof(GetFrequentCoActors))]
    public async Task<ActionResult<IEnumerable<CoActorResult>>> GetFrequentCoActors(int id, [FromQuery(Name = "")] PageQueryParameter page)
    {
        var coActor = await _personManager.GetFrequentCoActorsAsync(id, page.Number, page.Count);
        var totalCount = await _personManager.GetFrequentCoActorsCountAsync(id);

        foreach (var ca in coActor)
            ca.Links = AddPersonLinks(ca.Id);

        var result = _pagingHelper.CreatePaging(
            nameof(GetFrequentCoActors),
            page.Number, page.Count,
            totalCount,
            coActor,
            new { id }
        );

        return Ok(result);
    }

    private List<Link> AddPersonLinks(int personId)
        => [ new Link {
                Href = _pagingHelper.GetResourceLink(nameof(GetPersonById), new { id = personId }) ?? string.Empty,
                Rel = "self",
                Method = "GET"
            },
            new Link {
                Href = _pagingHelper.GetResourceLink(nameof(GetFrequentCoActors), new { id = personId }) ?? string.Empty,
                Rel = "frequent-coactors",
                Method = "GET"
            }
        ];

    private List<Link> AddMediaLinks(MediaBasicResult media)
        => [ new Link {
                Href = _pagingHelper.GetResourceLink(nameof(MediaController.Get), new { id = media.Id }) ?? string.Empty,
                Rel = "self",
                Method = "GET"
            }
        ];
}