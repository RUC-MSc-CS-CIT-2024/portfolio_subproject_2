using CitMovie.Models.DTOs;

namespace CitMovie.Business;

public interface IGenreManager
{
    Task<IEnumerable<GenreResult>> GetAllGenresAsync(int page, int pageSize);
    Task<int> GetTotalGenresCountAsync();
}
