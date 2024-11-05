namespace CitMovie.Data;

public interface IGenreRepository
{
    Task<IEnumerable<Genre>> GetAllGenresAsync(int page, int pageSize);
    Task<int> GetTotalGenresCountAsync();
}
