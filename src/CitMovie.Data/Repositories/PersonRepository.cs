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
}
