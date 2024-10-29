namespace CitMovie.Data;

public interface IPromotionalMediaRepository
{
    Task<IList<PromotionalMedia>> GetPromotionalMediaAsync(int page, int pageSize);
    Task<PromotionalMedia> GetPromotionalMediaByIdAsync(int id);
    Task<int> GetPromotionalMediaCountAsync();
}