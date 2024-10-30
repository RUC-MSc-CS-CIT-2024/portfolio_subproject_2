namespace CitMovie.Data.Repositories;

public interface IGenreRepository
{
    Task<IEnumerable<Genre>> GetAllGenresAsync(int page, int pageSize);
    Task<int> GetTotalGenresCountAsync();
}
