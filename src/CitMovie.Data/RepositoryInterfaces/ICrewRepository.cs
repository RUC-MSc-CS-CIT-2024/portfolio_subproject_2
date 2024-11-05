namespace CitMovie.Data;

public interface ICrewRepository {
    Task<List<CrewMember>> GetCrew(int mediaId, int page, int pageCount);
    Task<List<CastMember>> GetCast(int mediaId, int page, int pageCount);
}
