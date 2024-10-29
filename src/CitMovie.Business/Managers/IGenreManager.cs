using CitMovie.Models.DTOs;

namespace CitMovie.Business.Managers;

public interface IGenreManager
{
    Task<IList<GenreDto>> GetAllGenresAsync(int page, int pageSize);
    Task<int> GetTotalGenresCountAsync();
}
