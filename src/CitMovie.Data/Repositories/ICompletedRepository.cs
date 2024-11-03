namespace CitMovie.Data;

public interface ICompletedRepository
{
    Task<Completed> AddCompletedAsync(Completed completed);
    Task<Completed?> GetCompletedByIdAsync(int completedId);
    Task<IEnumerable<Completed>> GetUserCompletedItemsAsync(int userId, int page, int pageSize);
    Task<Completed> UpdateCompletedAsync(Completed completed);
    Task<bool> DeleteCompletedAsync(int completedId);
}
