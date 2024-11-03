namespace CitMovie.Data;

public interface IPromotionalMediaRepository
{
    Task<IEnumerable<PromotionalMedia>> GetPromotionalMediaOfMediaAsync(int mediaId, int page, int pageSize);
    Task<IList<PromotionalMedia>> GetPromotionalMediaOfReleaseAsync(int mediaId, int releaseId, int page, int pageSize);
    Task<PromotionalMedia> GetPromotionalMediaByIdAsync(int id, int? mediaId, int? releaseId);
    Task<bool> DeletePromotionalMediaAsync(int mediaId, int releaseid, int id);
    Task<PromotionalMedia> CreatePromotionalMediaAsync(int mediaId, int releaseId,PromotionalMedia model);
    Task<int> GetPromotionalMediaCountAsync(int id, string parameter);
}