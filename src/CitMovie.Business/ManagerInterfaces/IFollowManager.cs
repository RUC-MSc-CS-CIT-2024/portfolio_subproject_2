namespace CitMovie.Business;
public interface IFollowManager
{
    Task<IEnumerable<FollowResult>> GetFollowingsAsync(int userId, int page, int pageSize);
    Task<FollowResult> CreateFollowAsync(int userId, int personId);
    Task<bool> RemoveFollowingAsync(int userId, int followingId);
    Task<int> GetTotalFollowingsCountAsync(int userId);
}