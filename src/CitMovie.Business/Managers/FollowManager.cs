using CitMovie.Models.DTOs;
namespace CitMovie.Business;

public class FollowManager : IFollowManager
{
    private readonly IFollowRepository _followRepository;

    public FollowManager(IFollowRepository followRepository)
    {
        _followRepository = followRepository;
    }

    public async Task<IEnumerable<FollowResult>> GetFollowingsAsync(int userId, int page, int pageSize)
    {
        var followings = await _followRepository.GetFollowingsAsync(userId, page, pageSize);
        return followings.Select(f => new FollowResult
        {
            PersonId = f.PersonId,
            FollowingId = f.FollowingId,
            FollowedSince = f.FollowedSince
        }).ToList();
    }

    public async Task<FollowResult> CreateFollowAsync(int userId, int personId)
    {
        var follow = new Follow
        {
            UserId = userId,
            PersonId = personId,
            FollowedSince = DateTime.UtcNow
        };

        await _followRepository.CreateFollowAsync(follow);

        return new FollowResult
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