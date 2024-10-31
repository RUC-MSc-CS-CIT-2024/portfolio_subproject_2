namespace CitMovie.Business;

public interface IPersonManager
{

    Task<IEnumerable<PersonResult>> GetPersonsAsync(int page, int pageSize);
    Task<int> GetTotalPersonsCountAsync();
    Task<PersonResult?> GetPersonByIdAsync(int id);
    Task<IEnumerable<MediaResult>> GetMediaByPersonIdAsync(int id, int page, int pageSize);
    Task<int> GetMediaByPersonIdCountAsync(int id);
}
