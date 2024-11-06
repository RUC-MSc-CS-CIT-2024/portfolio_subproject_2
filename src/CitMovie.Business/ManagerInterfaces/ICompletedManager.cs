namespace CitMovie.Business;

public interface ICompletedManager
{
    Task<CompletedResult> MoveBookmarkToCompletedAsync(CompletedCreateRequest createCompletedDto);
    Task<CompletedResult?> GetCompletedAsync(int completedId);
    Task<IEnumerable<CompletedResult>> GetUserCompletedItemsAsync(int userId, int page, int pageSize);
    Task<CompletedResult?> UpdateCompletedAsync(int completedId, UpdateCompletedDto updateCompletedDto);
    Task<bool> DeleteCompletedAsync(int completedId);
    Task<int> GetTotalUserCompletedCountAsync(int userId);
}
