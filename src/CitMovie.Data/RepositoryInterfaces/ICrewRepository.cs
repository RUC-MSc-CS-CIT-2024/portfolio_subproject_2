namespace CitMovie.Data;

public interface ICrewRepository {
    Task<List<CrewMember>> GetCrewAsync(int mediaId, int page, int pageCount);
    Task<List<CastMember>> GetCastAsync(int mediaId, int page, int pageCount);
    Task<int> GetTotalCrewCountAsync(int id);
    Task<int> GetTotalCastCountAsync(int id);
}
