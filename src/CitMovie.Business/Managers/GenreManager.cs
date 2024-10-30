using CitMovie.Data.Repositories;
using CitMovie.Models.DTOs;

namespace CitMovie.Business.Managers;

public class GenreManager : IGenreManager
{
    private readonly IGenreRepository _genreRepository;

    public GenreManager(IGenreRepository genreRepository)
    {
        _genreRepository = genreRepository;
    }
    public async Task<IEnumerable<GenreDto>> GetAllGenresAsync(int page, int pageSize)
    {
        var genres = await _genreRepository.GetAllGenresAsync(page, pageSize);
        return genres.Select(g => new GenreDto
        {
            GenreId = g.GenreId,
            Name = g.Name
        }).ToList();
    }

    public async Task<int> GetTotalGenresCountAsync()
    {
        return await _genreRepository.GetTotalGenresCountAsync();
    }
}