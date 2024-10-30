using Microsoft.EntityFrameworkCore;

namespace CitMovie.Data.Repositories;

public class GenreRepository : IGenreRepository
{
    private readonly DataContext _context;

    public GenreRepository(DataContext context)
    {
        _context = context;
    }
    public async Task<IList<Genre>> GetAllGenresAsync(int page, int pageSize)
    {
        return await _context.Genres
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> GetTotalGenresCountAsync()
    {
        return await _context.Genres.CountAsync();
    }

}
