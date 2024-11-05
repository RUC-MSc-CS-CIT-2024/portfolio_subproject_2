namespace CitMovie.Business;

public interface IPromotionalMediaManager
{
    Task<IEnumerable<PromotionalMediaResult>> GetPromotionalMediaOfMediaAsync(int mediaId, int page, int pageSize);
    Task<IEnumerable<PromotionalMediaResult>> GetPromotionalMediaOfReleaseAsync(int mediaId, int releaseId, int page, int pageSize);
    Task<PromotionalMediaResult> GetPromotionalMediaByIdAsync( int id, int? mediaId, int? releaseId);
    Task<bool> DeletePromotionalMediaAsync(int mediaId, int releaseId, int id);

    Task<PromotionalMediaResult> CreatePromotionalMediaAsync(int mediaId, int releaseId, PromotionalMediaCreateRequest model);
    Task<int> GetPromotionalMediaCountAsync(int id, string parameter);
}