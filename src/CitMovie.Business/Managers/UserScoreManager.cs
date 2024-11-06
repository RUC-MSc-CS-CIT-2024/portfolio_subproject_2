namespace CitMovie.Business;

public class UserScoreManager : IUserScoreManager
{
    private readonly IUserScoreRepository _userScoreRepository;
    private readonly IMapper _mapper;

    public UserScoreManager(IUserScoreRepository userScoreRepository, IMapper mapper)
    {
        _userScoreRepository = userScoreRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserScoreResult>> GetScoresByUserIdAsync(int userId, int page, int pageSize, string? mediaType, int? mediaId, string? mediaName)
    {
        var userScores = await _userScoreRepository.GetUserScoresAsync(userId, page, pageSize, mediaType, mediaId, mediaName);
        return _mapper.Map<IEnumerable<UserScoreResult>>(userScores);
    }

    public async Task<int> GetTotalUserScoresCountAsync(int userId)
    {
        return await _userScoreRepository.GetTotalUserScoresCountAsync(userId);
    }

    public async Task CreateUserScoreAsync(int userId, UserScoreCreateRequest createRequest)
    {
        await _userScoreRepository.CreateUserScoreAsync(userId, createRequest.MediaId, createRequest.Score, createRequest.ReviewText);
    }

}