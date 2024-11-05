using Npgsql;

namespace CitMovie.Data;

public class CompletedRepository : ICompletedRepository
{
    private readonly FrameworkContext _context;

    public CompletedRepository(FrameworkContext context) =>
        _context = context;

public async Task<Completed> MoveBookmarkToCompletedAsync(int userId, int mediaId, int rewatchability, string? note = null)
{
    var sql = "SELECT move_bookmark_to_completed(@p_user_id, @p_media_id, @p_rewatchability, @p_note)";
    var parameters = new[]
    {
        new NpgsqlParameter("p_user_id", userId),
        new NpgsqlParameter("p_media_id", mediaId),
        new NpgsqlParameter("p_rewatchability", rewatchability),
        new NpgsqlParameter("p_note", note ?? (object)DBNull.Value)
    };

    await _context.Database.ExecuteSqlRawAsync(sql, parameters);

    var user = await _context.Users.FindAsync(userId);

    if (user == null)
    {
        throw new InvalidOperationException($"User with ID {userId} not found.");
    }

    var completed = new Completed
    {
        UserId = userId,
        MediaId = mediaId,
        Rewatchability = rewatchability,
        Note = note,
        User = user
    };

    return completed;
}

    public async Task<Completed?> GetCompletedByIdAsync(int completedId) =>
        await _context.Completed.FindAsync(completedId);

    public async Task<IEnumerable<Completed>> GetUserCompletedItemsAsync(int userId, int page, int pageSize) =>
        await _context.Completed
            .Where(c => c.UserId == userId)
            .AsNoTracking()
            .Pagination(page, pageSize)
            .ToListAsync();

    public async Task<Completed> UpdateCompletedAsync(Completed completed)
    {
        _context.Completed.Update(completed);
        await _context.SaveChangesAsync();
        return completed;
    }

    public async Task<bool> DeleteCompletedAsync(int completedId)
    {
        var completed = await _context.Completed.FindAsync(completedId);
        if (completed == null) return false;

        _context.Completed.Remove(completed);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<int> GetTotalUserCompletedCountAsync(int userId) =>
        await _context.Completed.CountAsync(c => c.UserId == userId);

}
