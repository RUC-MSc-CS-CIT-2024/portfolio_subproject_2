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


    public async Task<IEnumerable<PersonResult>> QueryPersonsAsync(PersonQueryParameter queryParameter, PageQueryParameter pageQuery, int? userId)
    {
        IEnumerable<Person> persons;
        if (queryParameter.Name != null) 
            persons = await _personRepository.GetPersonsByNameAsync(queryParameter.Name, userId, pageQuery.Number, pageQuery.Count);
        else 
            persons = await _personRepository.GetPersonsAsync(pageQuery.Number, pageQuery.Count);
        
        var PersonResults = _mapper.Map<IEnumerable<PersonResult>>(persons);
        return PersonResults;
    }

    public async Task<int> GetTotalPersonsCountAsync(PersonQueryParameter queryParameter)
    {
        if (queryParameter.Name != null)
            return await _personRepository.GetTotalPersonsCountAsync(queryParameter.Name);
        else
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

    public async Task<IEnumerable<PersonCrewResult>> GetMediaByPersonIdAsync(int id, int page, int pageSize)
    {
        IEnumerable<CrewBase> media = await _personRepository.GetMediaByPersonIdAsync(id, page, pageSize);

        IEnumerable<PersonCrewResult> crewResults = _mapper.Map<IEnumerable<PersonCrewResult>>(media.OfType<CrewMember>());
        IEnumerable<PersonCrewResult> castResults = _mapper.Map<IEnumerable<PersonCrewResult>>(media.OfType<CastMember>());

        return crewResults.Concat(castResults);
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
