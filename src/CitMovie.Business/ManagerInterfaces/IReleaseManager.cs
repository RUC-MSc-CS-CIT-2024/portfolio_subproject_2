namespace CitMovie.Business;

public interface IReleaseManager
{

    Task<List<ReleaseResult>> GetReleasesOfMediaAsync(int mediaId, int page, int pageSize);
    Task<ReleaseResult> GetReleaseOfMediaByIdAsync(int mediaId, int releaseId);
    Task<bool> DeleteReleaseOfMediaAsync(int mediaId, int releaseId);
    Task<ReleaseResult> CreateReleaseForMediaAsync(int mediaId, ReleaseCreateRequest release);
    Task<ReleaseResult> UpdateReleaseForMediaAsync(int mediaId, int releaseId, ReleaseUpdateRequest release);
    Task<int> GetReleasesCountAsync(int mediaId);

}