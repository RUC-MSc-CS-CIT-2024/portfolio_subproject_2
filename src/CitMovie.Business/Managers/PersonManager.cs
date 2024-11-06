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

    public async Task<IEnumerable<MediaBasicResult>> GetMediaByPersonIdAsync(int id, int page, int pageSize)
    {
        var media = await _personRepository.GetMediaByPersonIdAsync(id, page, pageSize);
        return _mapper.Map<IEnumerable<MediaBasicResult>>(media);
    }

    public async Task<int> GetMediaByPersonIdCountAsync(int id)
    {
        return await _personRepository.GetMediaByPersonIdCountAsync(id);
    }

    public async Task<IEnumerable<CoActorResult>> GetFrequentCoActorsAsync(int id, int page, int pageSize)
    {
        var coActors = await _personRepository.GetFrequentCoActorsAsync(id, page, pageSize);
        List<CoActorResult> result = [];

        foreach (var coActor in coActors) {
            CoActorResult r = _mapper.Map<CoActorResult>(coActor);
            int? tmpId = await _personRepository.GetPersonIdByImdbIdAsync(coActor.CoActorImdbId);
            if(tmpId == null)
                continue;
            r.Id = tmpId.Value;
            result.Add(r);
        }
            
        return result;
    }

    public async Task<int> GetFrequentCoActorsCountAsync(int id)
    {
        return await _personRepository.GetFrequentCoActorsCountAsync(id);
    }
}
