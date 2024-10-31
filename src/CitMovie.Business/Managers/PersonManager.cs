namespace CitMovie.Business;

public class PersonManager : IPersonManager
{
    private readonly IPersonRepository _personRepository;

    public PersonManager(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }


    public async Task<IEnumerable<PersonResult>> GetPersonsAsync(int page, int pageSize)

    {
        var persons = await _personRepository.GetPersonsAsync(page, pageSize);
        var totalCount = await _personRepository.GetTotalPersonsCountAsync();

        var PersonResults = persons.Select(p => new PersonResult
        (
            p.PersonId,
            p.Name,
            p.Description,
            p.Score,
            p.BirthDate,
            p.DeathDate
        )).ToList();

        return PersonResults;
    }

    public async Task<int> GetTotalPersonsCountAsync()
    {
        return await _personRepository.GetTotalPersonsCountAsync();
    }

    public async Task<PersonResult?> GetPersonByIdAsync(int id)
    {
        var p = await _personRepository.GetPersonByIdAsync(id);
        if (p == null)
        {
            return null;
        }

        return new PersonResult(
            p.PersonId,
            p.Name,
            p.Description,
            p.Score,
            p.BirthDate,
            p.DeathDate
        );
    }
}
