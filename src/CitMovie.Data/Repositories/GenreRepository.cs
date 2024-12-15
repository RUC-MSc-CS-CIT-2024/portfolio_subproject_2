namespace CitMovie.Data;

public class GenreRepository : IGenreRepository
{
    private readonly DataContext _context;

    public GenreRepository(DataContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Genre>> GetAllGenresAsync(int page, int pageSize)
    {
        return await _context.Genres
            .AsNoTracking()
            .OrderBy(g => g.Name)
            .ThenBy(g => g.GenreId)
            .Pagination(page, pageSize)
            .ToListAsync();
    }

    public async Task<int> GetTotalGenresCountAsync()
    {
        return await _context.Genres.CountAsync();
    }

}
