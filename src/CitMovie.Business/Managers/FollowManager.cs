using CitMovie.Data.FollowRepository;
using CitMovie.Models.DTOs;

namespace CitMovie.Business;

public class FollowManager
{
    private readonly IFollowRepository _followRepository;

    public FollowManager(IFollowRepository followRepository)
    {
        _followRepository = followRepository;
    }

    public async Task<IEnumerable<FollowDto>> GetFollowingsAsync(int userId, int page, int pageSize)
    {
        return await _followRepository.GetFollowingsAsync(userId, page, pageSize);
    }

    public async Task<FollowDto> CreateFollowAsync(int userId, int personId)
    {

        var follow = new Follow
        {
            UserId = userId,
            PersonId = personId,
            FollowedSince = DateTime.UtcNow
        };

        await _followRepository.CreateFollowAsync(follow);

        return new FollowDto
        {
            FollowingId = follow.FollowingId,
            PersonId = follow.PersonId,
            FollowedSince = follow.FollowedSince
        };
    }

    public async Task<bool> RemoveFollowingAsync(int userId, int followingId)
    {
        return await _followRepository.RemoveFollowingAsync(userId, followingId);
    }

    public async Task<int> GetTotalFollowingsCountAsync(int userId)
    {
        return await _followRepository.GetTotalFollowingsCountAsync(userId);
    }
}