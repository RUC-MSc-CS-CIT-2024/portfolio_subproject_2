using System;
using CitMovie.Data.Repositories;
using CitMovie.Models.DTOs;

namespace CitMovie.Business.Managers;

public class GenreManager
{
    private readonly IGenreRepository _genreRepository;

    public GenreManager(IGenreRepository genreRepository)
    {
        _genreRepository = genreRepository;
    }

    public async Task<IEnumerable<GenreDto>> GetAllGenresAsync(int page, int pageSize)
    {
        return await _genreRepository.GetAllGenresAsync(page, pageSize);
    }

    public async Task<int> GetTotalGenresCountAsync()
    {
        return await _genreRepository.GetTotalGenresCountAsync();
    }
}
