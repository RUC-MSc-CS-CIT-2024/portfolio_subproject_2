namespace CitMovie.Data;

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
            .OrderBy(c => c.Name)
            .ThenBy(c => c.CountryId)
            .Pagination(page, pageSize)
            .ToListAsync();
    }

    public async Task<int> GetTotalCountriesCountAsync()
    {
        return await _context.Countries.CountAsync();
    }
}