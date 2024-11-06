namespace CitMovie.Data;

public interface ICompletedRepository
{
    Task<Completed> MoveBookmarkToCompletedAsync(int userId, int mediaId, int rewatchability, string? note = null);
    Task<Completed?> GetCompletedByIdAsync(int completedId);
    Task<IEnumerable<Completed>> GetUserCompletedItemsAsync(int userId, int page, int pageSize);
    Task<Completed> UpdateCompletedAsync(Completed completed);
    Task<bool> DeleteCompletedAsync(int completedId);
    Task<int> GetTotalUserCompletedCountAsync(int userId);
}
