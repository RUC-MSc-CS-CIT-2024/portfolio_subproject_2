namespace CitMovie.Business;

public class UserScoreManager : IUserScoreManager
{
    private readonly IUserScoreRepository _userScoreRepository;

    public UserScoreManager(IUserScoreRepository userScoreRepository)
    {
        _userScoreRepository = userScoreRepository;
    }

    public async Task<IEnumerable<UserScoreResult>> GetScoresByUserIdAsync(int userId, int page, int pageSize, string? mediaType, int? mediaId, string? mediaName)
    {
        var userScores = await _userScoreRepository.GetUserScoresAsync(userId, page, pageSize, mediaType, mediaId, mediaName);
        return userScores.Select(us => new UserScoreResult(us.UserId, us.MediaId, us.ScoreValue, us.ReviewText)).ToList();
    }

    public async Task<int> GetTotalUserScoresCountAsync(int userId)
    {
        return await _userScoreRepository.GetTotalUserScoresCountAsync(userId);
    }

}