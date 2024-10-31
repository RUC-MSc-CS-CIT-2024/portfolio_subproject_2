namespace CitMovie.Business;

public interface IPromotionalMediaManager
{
    Task<IEnumerable<PromotionalMediaResultDto>> GetPromotionalMediaOfMediaAsync(int mediaId, int page, int pageSize);
    Task<IEnumerable<PromotionalMediaResultDto>> GetPromotionalMediaOfReleaseAsync(int mediaId, int releaseId, int page, int pageSize);
    Task<PromotionalMediaResultDto> GetPromotionalMediaByIdAsync( int id, int? mediaId, int? releaseId);
    Task<bool> DeletePromotionalMediaAsync(int mediaId, int releaseId, int id);

    Task<PromotionalMediaResultDto> CreatePromotionalMediaAsync(int mediaId, int releaseId, CreatePromotionalMediaDto model);
    Task<int> GetPromotionalMediaCountAsync(int id, string parameter);
}