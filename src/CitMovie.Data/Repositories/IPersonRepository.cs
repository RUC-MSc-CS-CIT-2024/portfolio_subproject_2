namespace CitMovie.Data;

public interface IPersonRepository
{
    Task<IEnumerable<Person>> GetPersonsAsync(int page, int pageSize);
    Task<int> GetTotalPersonsCountAsync();
    Task<Person?> GetPersonByIdAsync(int id);
    Task<IEnumerable<Media>> GetMediaByPersonIdAsync(int id, int page, int pageSize);
    Task<int> GetMediaByPersonIdCountAsync(int id);
    Task<string> GetActorNameByIdAsync(int id);
    Task<int?> GetPersonIdByImdbIdAsync(string imdbId);
    Task<IEnumerable<CoActor>> GetFrequentCoActorsAsync(string actorName, int page, int pageSize);
    Task<int> GetFrequentCoActorsCountAsync(string actorName);
}
