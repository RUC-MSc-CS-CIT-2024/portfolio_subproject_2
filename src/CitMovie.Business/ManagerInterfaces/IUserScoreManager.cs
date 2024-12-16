namespace CitMovie.Business;

public interface IUserScoreManager
{
    Task<IEnumerable<UserScoreResult>> GetScoresByUserIdAsync(int userId, int page, int pageSize, string? mediaType, int? mediaId, string? mediaName);
    Task<int> GetTotalUserScoresCountAsync(int userId, string? mediaType, int? mediaId, string? mediaName);
    Task CreateUserScoreAsync(int userId, UserScoreCreateRequest createRequest);
}

