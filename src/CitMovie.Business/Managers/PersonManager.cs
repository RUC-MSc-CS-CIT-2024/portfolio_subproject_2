namespace CitMovie.Business;

public class PersonManager : IPersonManager
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public PersonManager(IPersonRepository personRepository, IMapper mapper)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }


    public async Task<IEnumerable<PersonResult>> GetPersonsAsync(int page, int pageSize)

    {
        var persons = await _personRepository.GetPersonsAsync(page, pageSize);
        var PersonResults = _mapper.Map<IEnumerable<PersonResult>>(persons);
        return PersonResults;
    }

    public async Task<int> GetTotalPersonsCountAsync()
    {
        return await _personRepository.GetTotalPersonsCountAsync();
    }

    public async Task<PersonResult?> GetPersonByIdAsync(int id)
    {
        var person = await _personRepository.GetPersonByIdAsync(id);
        if (person == null)
        {
            return null;
        }
        return _mapper.Map<PersonResult>(person);

    }

    public async Task<IEnumerable<MediaResult>> GetMediaByPersonIdAsync(int id, int page, int pageSize)
    {
        var media = await _personRepository.GetMediaByPersonIdAsync(id, page, pageSize);
        return _mapper.Map<IEnumerable<MediaResult>>(media);
    }

    public async Task<int> GetMediaByPersonIdCountAsync(int id)
    {
        return await _personRepository.GetMediaByPersonIdCountAsync(id);
    }

    public async Task<string> GetActorNameByIdAsync(int id)
    {
        return await _personRepository.GetActorNameByIdAsync(id);
    }

    public async Task<int?> GetPersonIdByImdbIdAsync(string imdbId)
    {
        return await _personRepository.GetPersonIdByImdbIdAsync(imdbId);
    }

    public async Task<IEnumerable<CoActorResult>> GetFrequentCoActorsAsync(string actorName, int page, int pageSize)
    {
        var coActors = await _personRepository.GetFrequentCoActorsAsync(actorName, page, pageSize);
        return _mapper.Map<IEnumerable<CoActorResult>>(coActors);
    }

    public async Task<int> GetFrequentCoActorsCountAsync(int id)
    {
        var actorName = await GetActorNameByIdAsync(id);
        return await _personRepository.GetFrequentCoActorsCountAsync(actorName);
    }
}
