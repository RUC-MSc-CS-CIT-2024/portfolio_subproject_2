namespace CitMovie.Data;

public interface IFollowRepository
{
    Task<IEnumerable<Follow>> GetFollowingsAsync(int userId, int page, int pageSize);
    Task CreateFollowAsync(Follow follow);
    Task<bool> RemoveFollowingAsync(int userId, int followingId);
    Task<int> GetTotalFollowingsCountAsync(int userId);
}
