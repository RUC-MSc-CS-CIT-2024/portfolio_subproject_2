namespace CitMovie.Business;

public interface IPersonManager
{

    Task<IEnumerable<PersonResult>> GetPersonsAsync(int page, int pageSize);
    Task<int> GetTotalPersonsCountAsync();
    Task<PersonResult?> GetPersonByIdAsync(int id);
    Task<IEnumerable<PersonResult.MediaResult>> GetMediaByPersonIdAsync(int id, int page, int pageSize);
    Task<int> GetMediaByPersonIdCountAsync(int id);
    Task<string?> GetActorNameByIdAsync(int id);
    Task<int?> GetPersonIdByImdbIdAsync(string imdbId);
    Task<IEnumerable<CoActorResult>> GetFrequentCoActorsAsync(string actorName, int page, int pageSize);
    Task<int> GetFrequentCoActorsCountAsync(int id);
}
