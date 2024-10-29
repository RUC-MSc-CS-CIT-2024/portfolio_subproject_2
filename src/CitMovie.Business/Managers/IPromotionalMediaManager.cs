namespace CitMovie.Business;

public interface IPromotionalMediaManager
{
    Task<IEnumerable<PromotionalMediaDto>> GetPromotionalMediaAsync(int page, int pageSize);
    Task<PromotionalMediaDto> GetPromotionalMediaByIdAsync(int id);
    Task<int> GetPromotionalMediaCountAsync();
}