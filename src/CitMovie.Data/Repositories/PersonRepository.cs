namespace CitMovie.Data;

public class PersonRepository : IPersonRepository
{
    private readonly DataContext _dataContext;

    public PersonRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<IEnumerable<Person>> GetPersonsAsync(int page, int pageSize)
    {
        return await _dataContext.People
            .AsNoTracking()
            .OrderBy(p => p.PersonId)
            .Pagination(page, pageSize)
            .ToListAsync();
    }
    public async Task<int> GetTotalPersonsCountAsync()
    {
        return await _dataContext.People.CountAsync();
    }

    public async Task<Person?> GetPersonByIdAsync(int id)
    {
        return await _dataContext.People
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.PersonId == id);
    }

    public async Task<IEnumerable<Media>> GetMediaByPersonIdAsync(int id, int page, int pageSize)
    {
        var mediaQuery = _dataContext.Media
            .AsNoTracking()
            .Include(m => m.CrewMembers)
            .Include(m => m.CastMembers)
            .Include(m => m.Titles)
            .Include(m => m.Genres)
            .Where(m => m.CrewMembers.Any(cm => cm.PersonId == id) || m.CastMembers.Any(cm => cm.PersonId == id))
            .OrderBy(m => m.Id);

        return await mediaQuery
            .Pagination(page, pageSize)
            .ToListAsync();
    }

    public async Task<int> GetMediaByPersonIdCountAsync(int id)
    {
        return await _dataContext.Media
            .AsNoTracking()
            .Include(m => m.CrewMembers)
            .Include(m => m.CastMembers)
            .Include(m => m.Titles)
            .Where(m => m.CrewMembers.Any(cm => cm.PersonId == id) || m.CastMembers.Any(cm => cm.PersonId == id))
            .CountAsync();
    }

    public async Task<string?> GetActorNameByIdAsync(int id)
    {
        var coActor = await _dataContext.People
            .AsNoTracking()
            .Where(a => a.PersonId == id)
            .Select(a => a.Name)
            .FirstOrDefaultAsync();

        return coActor;
    }
    public async Task<int?> GetPersonIdByImdbIdAsync(string imdbId)
    {
        var person = await _dataContext.People
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.ImdbId == imdbId);

        return person?.PersonId;
    }

    public async Task<IEnumerable<CoActor>> GetFrequentCoActorsAsync(int id, int page, int pageSize)
    {
        Person p = _dataContext.People.First(x => x.PersonId == id);
        var coActors = await _dataContext.GetFrequentCoActors(p.Name)
            .AsNoTracking()
            .Pagination(page, pageSize)
            .ToListAsync();

        return coActors;
    }

    public async Task<int> GetFrequentCoActorsCountAsync(int id)
    {
        Person p = _dataContext.People.First(x => x.PersonId == id);
        var count = await _dataContext.GetFrequentCoActors(p.Name)
            .AsNoTracking()
            .CountAsync();

        return count;
    }
}
