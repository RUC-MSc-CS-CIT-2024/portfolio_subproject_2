using CitMovie.Models.DTOs;

namespace CitMovie.Data.Repositories;

public interface IGenreRepository
{
    Task<IList<GenreDto>> GetAllGenresAsync(int page, int pageSize);
    Task<int> GetTotalGenresCountAsync();
}
