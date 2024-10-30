namespace CitMovie.Business;

public class GenreManager : IGenreManager
{
    private readonly IGenreRepository _genreRepository;

    public GenreManager(IGenreRepository genreRepository)
    {
        _genreRepository = genreRepository;
    }
    public async Task<IEnumerable<GenreResult>> GetAllGenresAsync(int page, int pageSize)
    {
        var genres = await _genreRepository.GetAllGenresAsync(page, pageSize);
        return genres.Select(g => new GenreResult(g.GenreId, g.Name)).ToList();
    }

    public async Task<int> GetTotalGenresCountAsync()
    {
        return await _genreRepository.GetTotalGenresCountAsync();
    }
}
