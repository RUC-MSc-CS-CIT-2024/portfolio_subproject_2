using Microsoft.EntityFrameworkCore;

namespace CitMovie.Data;

public class ReleaseRepository : IReleaseRepository
{
    private readonly DataContext _context;

    public ReleaseRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<List<Release>> GetReleasesOfMediaAsync(int mediaId, int page, int pageSize)
    {
        try
        {
            await ValidateMediaAsync(mediaId);
            return await _context.Releases
                .Where(r => r.MediaId == mediaId)
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        catch (Exception e)
        {
            throw new InvalidOperationException(e.Message);
        }
    }

    public async Task<Release> GetReleaseOfMediaByIdAsync(int mediaId, int releaseId)
    {
        try
        {
            await ValidateMediaAsync(mediaId);
            var result = await _context.Releases
                .Where(r => r.MediaId == mediaId)
                .Where(r => r.ReleaseId == releaseId)
                .FirstOrDefaultAsync();
            
            return result ?? throw new Exception("No release with id " + mediaId + " was found");
        }
        catch (Exception e)
        {
            throw new InvalidOperationException(e.Message);
        }
    }

    private async Task ValidateMediaAsync(int mediaId)
    {
        var exists = await _context.Media.AnyAsync(m => m.Id == mediaId);
        if (!exists)
        {
            throw new Exception($"Media with id {mediaId} does not exist");
        }
    }
}