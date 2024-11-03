namespace CitMovie.Business;

public interface ICompletedManager
{
    Task<CompletedDto> CreateCompletedAsync(CreateCompletedDto createCompletedDto);
    Task<CompletedDto?> GetCompletedAsync(int completedId);
    Task<IEnumerable<CompletedDto>> GetUserCompletedItemsAsync(int userId, int page, int pageSize);
    Task<CompletedDto?> UpdateCompletedAsync(int completedId, UpdateCompletedDto updateCompletedDto);
    Task<bool> DeleteCompletedAsync(int completedId);
}
