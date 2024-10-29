using CitMovie.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CitMovie.Data.Repositories;

public class CountryRepository : ICountryRepository
{
    private readonly DataContext _context;

    public CountryRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<IList<CountryDto>> GetAllCountriesAsync(int page, int pageSize)
    {
        return await _context.Countries
            .Skip(page * pageSize)
            .Take(pageSize)
            .Select(c => new CountryDto
            {
                CountryId = c.CountryId,
                ImdbCountryCode = c.ImdbCountryCode,
                IsoCode = c.IsoCode,
                Name = c.Name
            })
            .ToListAsync();
    }


    public async Task<int> GetTotalCountriesCountAsync()
    {
        return await _context.Countries.CountAsync();
    }
}