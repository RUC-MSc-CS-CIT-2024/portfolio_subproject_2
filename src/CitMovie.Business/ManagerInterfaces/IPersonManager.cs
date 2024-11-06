namespace CitMovie.Business;

public interface IPersonManager
{

    Task<IEnumerable<PersonResult>> GetPersonsAsync(int page, int pageSize);
    Task<int> GetTotalPersonsCountAsync();
    Task<PersonResult?> GetPersonByIdAsync(int id);
    Task<IEnumerable<MediaBasicResult>> GetMediaByPersonIdAsync(int id, int page, int pageSize);
    Task<int> GetMediaByPersonIdCountAsync(int id);
    Task<IEnumerable<CoActorResult>> GetFrequentCoActorsAsync(int id, int page, int pageSize);
    Task<int> GetFrequentCoActorsCountAsync(int id);
}
