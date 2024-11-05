namespace CitMovie.Data;

public interface ISearchHistoryRepository
{
    Task<IEnumerable<SearchHistory>> GetUserSearchHistoriesAsync(int userId, int page, int pageSize);
    Task<int> GetUsersTotalSearchHistoriesCountAsync(int userId);
    Task<bool> DeleteUsersSearchHistoriesAsync(int userId, int searchHistoryId);
}
