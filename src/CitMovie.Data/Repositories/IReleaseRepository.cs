namespace CitMovie.Data;

public interface IReleaseRepository
{
    Task<List<Release>> GetReleasesOfMediaAsync(int mediaId, int page, int pageSize);
    Task<Release> GetReleaseOfMediaByIdAsync(int mediaId, int releaseId);
    Task<bool> DeleteReleaseOfMediaAsync(int mediaId, int releaseId);
}