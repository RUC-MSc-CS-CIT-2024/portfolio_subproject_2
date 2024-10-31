namespace CitMovie.Data;

public interface IPersonRepository
{
    Task<IEnumerable<Person>> GetPersonsAsync(int page, int pageSize);
    Task<int> GetTotalPersonsCountAsync();
}
