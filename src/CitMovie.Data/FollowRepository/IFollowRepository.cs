using CitMovie.Models.DTOs;
using CitMovie.Models.DomainObjects;

namespace CitMovie.Data.FollowRepository
{
    public interface IFollowRepository
    {
        Task<IList<FollowDto>> GetFollowingsAsync(int userId, int page, int pageSize);
        Task CreateFollowAsync(Follow follow);
        Task<bool> RemoveFollowingAsync(int userId, int followingId);
        Task<int> GetTotalFollowingsCountAsync(int userId);
    }
}