namespace CitMovie.Business;

public interface ISearchHistoryManager
{
    Task<IEnumerable<SearchHistoryResult>> GetUserSearchHistoriesAsync(int userId, int page, int pageSize);
    Task<int> GetUsersTotalSearchHistoriesCountAsync(int userId);
    Task<bool> DeleteUsersSearchHistoriesAsync(int userId, int searchHistoryId);
}
