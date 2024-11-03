namespace CitMovie.Business;

public interface IUserScoreManager
{
    Task<IEnumerable<UserScoreResult>> GetScoresByUserIdAsync(int userId, int page, int pageSize, string? mediaType, int? mediaId, string? mediaName);
    Task<int> GetTotalUserScoresCountAsync(int userId);
    Task CreateUserScoreAsync(int userId, string imdbId, int score, string reviewText);
}
