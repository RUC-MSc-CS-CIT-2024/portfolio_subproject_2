namespace CitMovie.Business;

public class FollowManager : IFollowManager
{
    private readonly IFollowRepository _followRepository;
    private readonly IMapper _mapper;

    public FollowManager(IFollowRepository followRepository, IMapper mapper)
    {
        _followRepository = followRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<FollowResult>> GetFollowingsAsync(int userId, int page, int pageSize)
    {
        var followings = await _followRepository.GetFollowingsAsync(userId, page, pageSize);
        return _mapper.Map<IEnumerable<FollowResult>>(followings);
    }

    public async Task<FollowResult> CreateFollowAsync(int userId, int personId)
    {
        Follow follow = new()
        {
            UserId = userId,
            PersonId = personId,
            FollowedSince = DateTime.UtcNow
        };

        await _followRepository.CreateFollowAsync(follow);
        return _mapper.Map<FollowResult>(follow);
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