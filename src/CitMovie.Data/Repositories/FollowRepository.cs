namespace CitMovie.Data;

public class FollowRepository : IFollowRepository
{
    private readonly FrameworkContext _context;

    public FollowRepository(FrameworkContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Follow>> GetFollowingsAsync(int userId, int page, int pageSize)
    {
        return await _context.Follows
            .AsNoTracking()
            .Where(f => f.UserId == userId)
            .Pagination(page, pageSize)
            .ToListAsync();
    }

    public async Task<int> GetTotalFollowingsCountAsync(int userId)
    {
        return await _context.Follows.CountAsync(f => f.UserId == userId);
    }

    public async Task CreateFollowAsync(Follow follow)
    {
        await _context.Follows.AddAsync(follow);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> RemoveFollowingAsync(int userId, int followingId)
    {
        var follow = await _context.Follows
            .FirstOrDefaultAsync(f => f.UserId == userId && f.FollowingId == followingId);

        if (follow == null)
        {
            return false;
        }

        _context.Follows.Remove(follow);
        await _context.SaveChangesAsync();

        return true;
    }
}