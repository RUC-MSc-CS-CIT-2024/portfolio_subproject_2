namespace CitMovie.Data;
public interface IUserScoreRepository
{
    Task<IEnumerable<UserScore>> GetUserScoresAsync(int userId, int page, int pageSize, string? mediaType, int? mediaId, string? mediaName);
    Task<int> GetTotalUserScoresCountAsync(int userId, string? mediaType, int? mediaId, string? mediaName);
    Task CreateUserScoreAsync(int userId, int mediaId, int score, string? reviewText);
}
