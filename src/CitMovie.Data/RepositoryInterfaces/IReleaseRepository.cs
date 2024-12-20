namespace CitMovie.Data;

public interface IReleaseRepository
{
    Task<List<Release>> GetReleasesOfMediaAsync(int mediaId, int page, int pageSize);
    Task<Release> GetReleaseOfMediaByIdAsync(int mediaId, int releaseId);
    Task<bool> DeleteReleaseOfMediaAsync(int mediaId, int releaseId);
    Task<Release> CreateReleaseForMediaAsync(int mediaId, Release release);
    Task<Release> UpdateReleaseForMediaAsync(int mediaId, int releaseId, Release release);
    Task<Release> GetReleaseByIdAsync(int releaseId);
    Task<int> GetReleasesCountAsync(int mediaId);
}