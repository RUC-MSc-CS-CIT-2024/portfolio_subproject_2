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

    [HttpGet(Name = nameof(QueryPerson))]
    public async Task<ActionResult<IEnumerable<PersonResult>>> QueryPerson([FromQuery] PersonQueryParameter queryParameter, [FromQuery(Name = "")] PageQueryParameter page)
    {
        
        var persons = await _personManager.QueryPersonsAsync(queryParameter, page, GetUserId());
        var totalCount = await _personManager.GetTotalPersonsCountAsync(queryParameter);

        var result = _pagingHelper.CreatePaging(
            nameof(QueryPerson),
            page.Number, page.Count,
            totalCount,
            persons
        );

        foreach (var person in persons)
            person.Links = Url.AddPersonLinks(person.Id);

        return Ok(result);
    }

    [HttpGet("{id}", Name = nameof(GetPersonById))]
    public async Task<ActionResult<PersonResult>> GetPersonById(int id)
    {
        var person = await _personManager.GetPersonByIdAsync(id);
        if (person == null)
            return NotFound();

        person.Links = Url.AddPersonLinks(id);

        return Ok(person);
    }

    [HttpGet("{id}/media", Name = nameof(GetMediaByPersonId))]
    public async Task<ActionResult<IEnumerable<PersonCrewResult>>> GetMediaByPersonId(int id, [FromQuery(Name = "")] PageQueryParameter page)
    {
        IEnumerable<PersonCrewResult> media = await _personManager.GetMediaByPersonIdAsync(id, page.Number, page.Count);
        var totalCount = await _personManager.GetMediaByPersonIdCountAsync(id);

        var result = _pagingHelper.CreatePaging(
            nameof(GetMediaByPersonId),
            page.Number, page.Count,
            totalCount,
            media,
            new { id }
        );

        foreach (PersonCrewResult m in media) {
            if (m.Media is null)
                continue;
            m.Links = Url.AddCrewAndCastLinks(m.Media.Id, id);
            m.Media.Links = Url.AddMediaLinks(m.Media.Id);
        }

        return Ok(result);
    }

    [HttpGet("{id}/coactors", Name = nameof(GetFrequentCoActors))]
    public async Task<ActionResult<IEnumerable<CoActorResult>>> GetFrequentCoActors(int id, [FromQuery(Name = "")] PageQueryParameter page)
    {
        var coActor = await _personManager.GetFrequentCoActorsAsync(id, page.Number, page.Count);
        var totalCount = await _personManager.GetFrequentCoActorsCountAsync(id);

        foreach (var ca in coActor)
            ca.Links = Url.AddPersonLinks(ca.Id);

        var result = _pagingHelper.CreatePaging(
            nameof(GetFrequentCoActors),
            page.Number, page.Count,
            totalCount,
            coActor,
            new { id }
        );

        return Ok(result);
    }

    private int? GetUserId()
    {
        string? userIdString = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
        int? userId = null;
        if (int.TryParse(userIdString, out int parseResult))
            userId = parseResult;
        return userId;
    }
}
