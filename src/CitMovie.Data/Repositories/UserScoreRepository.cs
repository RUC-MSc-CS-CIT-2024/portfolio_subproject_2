using Microsoft.EntityFrameworkCore;

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
        // Step 1: Retrieve user scores from FrameworkContext
        var userScoresQuery = _frameworkContext.UserScores
            .Where(us => us.UserId == userId)
            .AsQueryable();

        // Step 2: Retrieve media and titles from DataContext with filters applied
        var mediaQuery = _dataContext.Media.AsQueryable();
        if (!string.IsNullOrEmpty(mediaType))
        {
            mediaQuery = mediaQuery.Where(m => m.Type == mediaType);
        }
        if (mediaId > 0)
        {
            mediaQuery = mediaQuery.Where(m => m.Id == mediaId);
        }
        var mediaList = await mediaQuery.ToListAsync();

        var titlesQuery = _dataContext.Titles.AsQueryable();
        if (!string.IsNullOrEmpty(mediaName))
        {
            titlesQuery = titlesQuery.Where(t => t.Name.Contains(mediaName));
        }
        var titlesList = await titlesQuery.ToListAsync();

        // Step 3: Perform the join in memory using HashSet for efficient lookups
        var mediaIds = new HashSet<int>(mediaList.Select(m => m.Id));
        var titleMediaIds = new HashSet<int>(titlesList.Select(t => t.MediaId));

        var userScoresList = await userScoresQuery.ToListAsync();
        var filteredUserScores = userScoresList
            .Where(us => mediaIds.Contains(us.MediaId) && (string.IsNullOrEmpty(mediaName) || titleMediaIds.Contains(us.MediaId)))
            .ToList();

        // Apply pagination
        var result = filteredUserScores
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToList();

        return result;
    }
    public async Task<int> GetTotalUserScoresCountAsync(int userId)
    {
        return await _frameworkContext.UserScores
            .CountAsync(us => us.UserId == userId);
    }
}
