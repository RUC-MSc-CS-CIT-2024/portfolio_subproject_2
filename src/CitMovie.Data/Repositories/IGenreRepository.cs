namespace CitMovie.Data.Repositories;

public interface IGenreRepository
{
    Task<IList<Genre>> GetAllGenresAsync(int page, int pageSize);
    Task<int> GetTotalGenresCountAsync();
}
