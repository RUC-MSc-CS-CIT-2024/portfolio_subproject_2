using Microsoft.EntityFrameworkCore;

namespace CitMovie.Data.Repositories;

public class CountryRepository : ICountryRepository
{
    private readonly DataContext _context;

    public CountryRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Country>> GetAllCountriesAsync(int page, int pageSize)
    {
        return await _context.Countries
            .AsNoTracking()
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> GetTotalCountriesCountAsync()
    {
        return await _context.Countries.CountAsync();
    }
}