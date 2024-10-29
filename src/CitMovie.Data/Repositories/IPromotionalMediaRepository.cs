namespace CitMovie.Data;

public interface IPromotionalMediaRepository
{
    Task<IList<PromotionalMedia>> GetPromotionalMediaAsync(int page, int pageSize);
    Task<PromotionalMedia> GetPromotionalMediaByIdAsync(int id);
    Task<bool> DeletePromotionalMediaAsync(int id);
    Task<PromotionalMedia> CreatePromotionalMediaAsync(PromotionalMedia model);
    Task<int> GetPromotionalMediaCountAsync();
}