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

    public async Task<IEnumerable<Person>> GetPersonsByNameAsync(string name, int? userId, int page, int pageSize) {
        IQueryable<int> ids = _dataContext
            .PersonSearch(name, userId)
            .Pagination(page, pageSize)
            .Select(x => x.Id)
            .Distinct();;
        
        return await _dataContext.People
            .AsNoTracking()
            .Where(x => ids.Contains(x.PersonId))
            .ToListAsync();
    }

    public async Task<int> GetTotalPersonsCountAsync()
        => await _dataContext.People.CountAsync();

    public async Task<int> GetTotalPersonsCountAsync(string name)
        => await _dataContext
            .PersonSearch(name, null)
            .CountAsync();

    public async Task<Person?> GetPersonByIdAsync(int id)
    {
        return await _dataContext.People
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.PersonId == id);
    }

    public async Task<IEnumerable<CrewBase>> GetMediaByPersonIdAsync(int id, int page, int pageSize)
    {
        var crew = _dataContext.CrewMembers
            .AsNoTracking()
            .Include(cm => cm.Media!)
            .ThenInclude(m => m.PrimaryInformation!)
            .ThenInclude(pi => pi.Release)
            .Where(cm => cm.PersonId == id)
            .Select(cm => new {
                cm.Id,
                Type = "crew",
                cm.Media!.PrimaryInformation!.Release!.ReleaseDate
            });
        
        var cast = _dataContext.CastMembers
            .AsNoTracking()
            .Include(cm => cm.Media!)
            .ThenInclude(m => m.PrimaryInformation!)
            .ThenInclude(pi => pi.Release)
            .Where(cm => cm.PersonId == id)
            .Select(cm => new {
                cm.Id,
                Type = "cast",
                cm.Media!.PrimaryInformation!.Release!.ReleaseDate
            });
        
        var ids = crew.Concat(cast)
            .OrderByDescending(x => x.ReleaseDate)
            .Pagination(page, pageSize);

        List<CrewBase> result = new();
        
        int[] crewIds = ids
            .Where(x => x.Type == "crew")
            .Select(cm => cm.Id).ToArray();
        if (crewIds.Length > 0) {
            List<CrewMember> crewResult = await _dataContext.CrewMembers
                .AsNoTracking()
                .Where(cm => crewIds.Contains(cm.Id))
                .Include(cm => cm.JobCategory)
                .Include(cm => cm.Media!)
                .ThenInclude(m => m.PrimaryInformation!)
                .ThenInclude(pi => pi.Title)
                .Include(cm => cm.Media!)
                .ThenInclude(m => m.PrimaryInformation!)
                .ThenInclude(pi => pi.Release)
                .Include(cm => cm.Media!)
                .ThenInclude(m => m.PrimaryInformation!)
                .ThenInclude(pi => pi.PromotionalMedia)
                .Include(cm => cm.Media!)
                .ThenInclude(x => (x as Episode)!.Season)
                .ToListAsync();
            result.AddRange(crewResult);
        }


        int[] castIds = ids
            .Where(x => x.Type == "cast")
            .Select(cm => cm.Id).ToArray();
        if (castIds.Length > 0) {
            List<CastMember> castResult = await _dataContext.CastMembers
                .AsNoTracking()
                .Where(cm => castIds.Contains(cm.Id))
                .Include(cm => cm.Media!)
                .ThenInclude(m => m.PrimaryInformation!)
                .ThenInclude(pi => pi.Title)
                .Include(cm => cm.Media!)
                .ThenInclude(m => m.PrimaryInformation!)
                .ThenInclude(pi => pi.Release)
                .Include(cm => cm.Media!)
                .ThenInclude(m => m.PrimaryInformation!)
                .ThenInclude(pi => pi.PromotionalMedia)
                .Include(cm => cm.Media!)
                .ThenInclude(x => (x as Episode)!.Season)
                .ToListAsync();
            result.AddRange(castResult);
        }
        
        return result
            .OrderByDescending(x => x.Media!.PrimaryInformation!.Release!.ReleaseDate)
            .ToList();
    }

    public async Task<int> GetMediaByPersonIdCountAsync(int id)
    {
        var crew = _dataContext.CrewMembers
            .AsNoTracking()
            .Where(cm => cm.PersonId == id)
            .Select(cm => new {
                cm.Id,
                Type = "crew"
            });
        
        var cast = _dataContext.CastMembers
            .AsNoTracking()
            .Where(cm => cm.PersonId == id)
            .Select(cm => new {
                cm.Id,
                Type = "cast"
            });
        
        return await crew.Concat(cast).CountAsync();
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
