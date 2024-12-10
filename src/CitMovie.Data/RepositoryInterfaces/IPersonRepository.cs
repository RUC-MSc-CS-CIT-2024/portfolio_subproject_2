namespace CitMovie.Data;

public interface IPersonRepository
{
    Task<IEnumerable<Person>> GetPersonsAsync(int page, int pageSize);
    Task<int> GetTotalPersonsCountAsync();
    Task<int> GetTotalPersonsCountAsync(string name);
    Task<Person?> GetPersonByIdAsync(int id);
    Task<IEnumerable<CrewBase>> GetMediaByPersonIdAsync(int id, int page, int pageSize);
    Task<int> GetMediaByPersonIdCountAsync(int id);
    Task<int?> GetPersonIdByImdbIdAsync(string imdbId);
    Task<IEnumerable<CoActor>> GetFrequentCoActorsAsync(int id, int page, int pageSize);
    Task<int> GetFrequentCoActorsCountAsync(int id);
    Task<IEnumerable<Person>> GetPersonsByNameAsync(string name, int? userId, int number, int count);
}
