using CitMovie.Models;

namespace CitMovie.Api;

[ApiController]
[Route("api/persons")]
public class PersonController : ControllerBase
{
    private readonly IPersonManager _personManager;
    private readonly PagingHelper _pagingHelper;

    public PersonController(IPersonManager personManager, PagingHelper pagingHelper)
    {
        _personManager = personManager;
        _pagingHelper = pagingHelper;
    }

    [HttpGet(Name = nameof(GetPersons))]
    public async Task<ActionResult<IEnumerable<PersonResult>>> GetPersons(
        [FromQuery] int page = 0,
        [FromQuery] int pageSize = 10)
    {
        var persons = await _personManager.GetPersonsAsync(page, pageSize);
        var totalCount = await _personManager.GetTotalPersonsCountAsync();

        var result = _pagingHelper.CreatePaging(
            nameof(GetPersons),
            page,
            pageSize,
            totalCount,
            persons
        );

        foreach (var person in persons)
        {
            AddPersonLinks(person);
        }

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

        AddPersonLinks(person);

        return Ok(person);
    }

    [HttpGet("{id}/media", Name = nameof(GetMediaByPersonId))]
    public async Task<ActionResult<IEnumerable<PersonResult.MediaResult>>> GetMediaByPersonId(int id,
          [FromQuery] int page = 0,
          [FromQuery] int pageSize = 10)
    {
        var media = await _personManager.GetMediaByPersonIdAsync(id, page, pageSize);
        var totalCount = await _personManager.GetMediaByPersonIdCountAsync(id);

        var result = _pagingHelper.CreatePaging(
            nameof(GetMediaByPersonId),
            page,
            pageSize,
            totalCount,
            media,
            new { id }
        );

        return Ok(result);
    }

    [HttpGet("{id}/coactors", Name = nameof(GetFrequentCoActors))]
    public async Task<ActionResult<IEnumerable<CoActorResult>>> GetFrequentCoActors(
      int id,
      [FromQuery] int page = 0,
      [FromQuery] int pageSize = 10)
    {
        var actorName = await _personManager.GetActorNameByIdAsync(id);
        var coActor = await _personManager.GetFrequentCoActorsAsync(actorName, page, pageSize);
        var totalCount = await _personManager.GetFrequentCoActorsCountAsync(id);

        foreach (var ca in coActor)
        {
            await AddCoActorLinks(ca);
        }

        var result = _pagingHelper.CreatePaging(
            nameof(GetFrequentCoActors),
            page,
            pageSize,
            totalCount,
            coActor,
            new { id }
        );

        return Ok(result);
    }

    private void AddPersonLinks(PersonResult person)
    {
        person.Links.Add(new Link
        {
            Href = _pagingHelper.GetResourceLink(nameof(GetPersonById), new { id = person.Id }) ?? string.Empty,
            Rel = "self",
            Method = "GET"
        });

        person.Links.Add(new Link
        {
            Href = _pagingHelper.GetResourceLink(nameof(GetFrequentCoActors), new { id = person.Id }) ?? string.Empty,
            Rel = "frequent-coactors",
            Method = "GET"
        });
    }

    private async Task AddCoActorLinks(CoActorResult coActor)
    {
        var personId = await _personManager.GetPersonIdByImdbIdAsync(coActor.Id);
        if (personId.HasValue)
        {
            coActor.Links.Add(new Link
            {
                Href = _pagingHelper.GetResourceLink(nameof(GetPersonById), new { id = personId }) ?? string.Empty,
                Rel = "self",
                Method = "GET"
            });
        }
    }
}