namespace CitMovie.Data;

public class UserScoreRepository : IUserScoreRepository
{
    private readonly FrameworkContext _frameworkContext;
    private readonly DataContext _dataContext;

    public UserScoreRepository(FrameworkContext frameworkContext, DataContext dataContext)
    {
        _frameworkContext = frameworkContext;
        _dataContext = dataContext;
    }


    public async Task<IEnumerable<UserScore>> GetUserScoresAsync(int userId, int page, int pageSize, string? mediaType, int? mediaId, string? mediaName)
    {
        var userScoresQuery = _frameworkContext.UserScores
            .Where(us => us.UserId == userId);

        var mediaQuery = _dataContext.Media.AsQueryable();
        if (!string.IsNullOrEmpty(mediaType))
        {
            mediaQuery = mediaQuery.Where(m => m.Type == mediaType);
        }
        if (mediaId.HasValue && mediaId > 0)
        {
            mediaQuery = mediaQuery.Where(m => m.Id == mediaId);
        }

        var titlesQuery = _dataContext.Titles.AsQueryable();
        if (!string.IsNullOrEmpty(mediaName))
        {
            titlesQuery = titlesQuery.Where(t => t.Name.Contains(mediaName));
        }

        var mediaIds = new HashSet<int>(await mediaQuery.Select(m => m.Id).ToListAsync());
        var titleMediaIds = new HashSet<int>(await titlesQuery.Select(t => t.MediaId).ToListAsync());
        
        var filteredUserScores = await userScoresQuery
            .Where(us => mediaIds.Contains(us.MediaId) && (string.IsNullOrEmpty(mediaName) || titleMediaIds.Contains(us.MediaId)))
            .Pagination(page, pageSize)
            .ToListAsync();

        return filteredUserScores;
    }

    public async Task<int> GetTotalUserScoresCountAsync(int userId)
    {
        return await _frameworkContext.UserScores
            .CountAsync(us => us.UserId == userId);
    }

    public async Task CreateUserScoreAsync(int userId, int mediaId, int score, string? reviewText)
    {
        Media m = _dataContext.Media.AsNoTracking().First(x => x.Id == mediaId);
        if (m.ImdbId == null)
            throw new InvalidOperationException();

        await _frameworkContext.Database.ExecuteSqlInterpolatedAsync(
            $"SELECT rate({userId}, {m.ImdbId}, {score}, {reviewText})");
    }
}
