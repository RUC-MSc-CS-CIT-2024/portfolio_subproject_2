using CitMovie.Data.FollowRepository;
using CitMovie.Models.DomainObjects;

namespace CitMovie.Business;

public class FollowService
{
    private readonly IFollowRepository _followRepository;

    public FollowService(IFollowRepository followRepository)
    {
        _followRepository = followRepository;
    }

    public async Task<IEnumerable<Follow>> GetFollowings(int userId, int page, int pageSize)
    {
        return await _followRepository.GetFollowings(userId, page, pageSize);
    }

    public async Task<Follow> CreateFollow(int userId, int personId)
    {
        return await _followRepository.CreateFollow(userId, personId);
    }

    public async Task<bool> RemoveFollowing(int userId, int followingId)
    {
        return await _followRepository.RemoveFollowing(userId, followingId);
    }

    public async Task<int> GetTotalFollowingsCount(int userId)
    {
        return await _followRepository.GetTotalFollowingsCount(userId);
    }
}