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
}
