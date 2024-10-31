namespace CitMovie.Data;

public interface IReleaseRepository
{
    Task<List<Release>> GetReleasesOfMediaAsync(int mediaId, int page, int pageSize);
}