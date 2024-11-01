using Microsoft.EntityFrameworkCore;

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
            .Skip(page * pageSize)
            .Take(pageSize)
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
        return await _dataContext.Media
            .AsNoTracking()
            .Include(m => m.CrewMembers)
            .Include(m => m.CastMembers)
            .Include(m => m.Title)
            .Where(m => m.CrewMembers.Any(cm => cm.PersonId == id) || m.CastMembers.Any(cm => cm.PersonId == id))
            .OrderBy(m => m.Id)
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> GetMediaByPersonIdCountAsync(int id)
    {
        return await _dataContext.Media
            .AsNoTracking()
            .Include(m => m.CrewMembers)
            .Include(m => m.CastMembers)
            .Include(m => m.Title)
            .Where(m => m.CrewMembers.Any(cm => cm.PersonId == id) || m.CastMembers.Any(cm => cm.PersonId == id))
            .CountAsync();
    }

    public async Task<string> GetActorNameByIdAsync(int id)
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
        var personId = await _dataContext.People
            .Where(p => p.ImdbId == imdbId)
            .Select(p => p.PersonId)
            .FirstOrDefaultAsync();

        return personId;
    }



    public async Task<IEnumerable<CoActor>> GetFrequentCoActorsAsync(string actorName, int page, int pageSize)
    {
        var coActors = await _dataContext.CoActors
            .FromSqlInterpolated($@"
            SELECT *
            FROM get_frequent_coplaying_actors({actorName})
            ")
            .AsNoTracking()
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return coActors;
    }

    public async Task<int> GetFrequentCoActorsCountAsync(string actorName)
    {
        var count = await _dataContext.CoActors
            .FromSqlInterpolated($@"
            SELECT coactor_imdb_id
            FROM get_frequent_coplaying_actors({actorName})")
            .AsNoTracking()
            .CountAsync();

        return count;
    }
}
