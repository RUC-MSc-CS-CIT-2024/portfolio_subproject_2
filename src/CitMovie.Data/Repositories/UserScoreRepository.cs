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
        if (page < 1 || pageSize < 1)
            return [];

        var userScoresQuery = _frameworkContext.UserScores
            .AsNoTracking()
            .Where(us => us.UserId == userId);

        if (mediaId.HasValue) {
            Media? m = _dataContext.Media.Include(x => x.Titles).FirstOrDefault(m => m.Id == mediaId);
            if (!string.IsNullOrEmpty(mediaType) && m is not null && m.Type != mediaType)
                return [];
            else if (!string.IsNullOrEmpty(mediaName) && m is not null && !m.Titles.Any(t => t.Name.Contains(mediaName)))
                return [];
            IQueryable<UserScore> userScoreWithIdQuery = userScoresQuery.Where(us => us.MediaId == mediaId);
            // No need to paginate as there can only be one result with MediaId 
            UserScore? us = userScoreWithIdQuery.FirstOrDefault(us => us.CreatedAt == userScoreWithIdQuery.Max(us => us.CreatedAt));
            return us is null ? [] : [us];
        }

        List<int> ids = [];
        if (!string.IsNullOrEmpty(mediaType)) {
            var mediaTypeMatchIds = await _dataContext.Media
                .Where(m => m.Type == mediaType)
                .Select(m => m.Id)
                .ToListAsync();
            ids.AddRange(mediaTypeMatchIds);
        }
        if (!string.IsNullOrEmpty(mediaName)) {
            var titleMatchIds = await _dataContext.Titles
                .Where(t => t.Name.Contains(mediaName))
                .Select(t => t.MediaId)
                .ToListAsync();
            ids.AddRange(titleMatchIds);
        }
        if (ids.Count > 0) {
            ids = ids.Distinct().ToList();
            userScoresQuery = userScoresQuery.Where(us => ids.Contains(us.MediaId));
        }
        
        var filteredUserScores = await userScoresQuery
            .GroupBy(us => us.MediaId)
            .Select(g => g.First(us => us.CreatedAt == g.Max(us => us.CreatedAt)))
            .ToListAsync();

        return filteredUserScores
            .AsQueryable()
            .OrderBy(us => us.UserScoreId)
            .Pagination(page, pageSize)
            .ToList();
    }

    public async Task<int> GetTotalUserScoresCountAsync(int userId, string? mediaType, int? mediaId, string? mediaName)
    {
        var userScoresQuery = _frameworkContext.UserScores
            .Where(us => us.UserId == userId);

        if(mediaId.HasValue) {
            Media? m = _dataContext.Media.Include(x => x.Titles).FirstOrDefault(m => m.Id == mediaId);
            if (!string.IsNullOrEmpty(mediaType) && m is not null && m.Type != mediaType)
                return 0;
            else if (!string.IsNullOrEmpty(mediaName) && m is not null && !m.Titles.Any(t => t.Name.Contains(mediaName)))
                return 0;
            return _frameworkContext.UserScores.Any(us => us.UserId == userId && us.MediaId == mediaId) ? 1 : 0;
        }

        List<int> ids = [];
        if (!string.IsNullOrEmpty(mediaType)) {
            var mediaTypeMatchIds = await _dataContext.Media
                .Where(m => m.Type == mediaType)
                .Select(m => m.Id)
                .ToListAsync();
            ids.AddRange(mediaTypeMatchIds);
        }
        if (!string.IsNullOrEmpty(mediaName)) {
            var titleMatchIds = await _dataContext.Titles
                .Where(t => t.Name.Contains(mediaName))
                .Select(t => t.MediaId)
                .ToListAsync();
            ids.AddRange(titleMatchIds);
        }
        if (ids.Count > 0) {
            ids = ids.Distinct().ToList();
            userScoresQuery = userScoresQuery.Where(us => ids.Contains(us.MediaId));
        }

        return await userScoresQuery
            .GroupBy(us => us.MediaId)
            .CountAsync();
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
