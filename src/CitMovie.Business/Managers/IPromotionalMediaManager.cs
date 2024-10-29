namespace CitMovie.Business;

public interface IPromotionalMediaManager
{
    Task<IEnumerable<PromotionalMediaDto>> GetPromotionalMediaAsync(int page, int pageSize);
    Task<PromotionalMediaDto> GetPromotionalMediaByIdAsync(int id);
    Task<bool> DeletePromotionalMediaAsync(int id);

    Task<PromotionalMediaDto> CreatePromotionalMediaAsync(CreatePromotionalMediaDto model);
    Task<int> GetPromotionalMediaCountAsync();
}