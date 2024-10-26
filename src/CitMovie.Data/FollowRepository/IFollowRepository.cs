using CitMovie.Models.DomainObjects;


namespace CitMovie.Data.FollowRepository
{
    public interface IFollowRepository
    {
        Task<IList<Follow>> GetFollowings(int userId, int page, int pageSize);
        Task<int> GetTotalFollowingsCount(int userId);
        Task<Follow> CreateFollow(int userId, int personId);
        Task<bool> RemoveFollowing(int userId, int followingId);
    }
}