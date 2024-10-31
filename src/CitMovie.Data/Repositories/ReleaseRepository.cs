using System.Reflection;
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
                .FirstOrDefaultAsync(r => r.ReleaseId == releaseId);
            
            return result ?? throw new Exception("No release with id " + mediaId + " was found");
        }
        catch (Exception e)
        {
            throw new InvalidOperationException(e.Message);
        }
    }

    public async Task<bool> DeleteReleaseOfMediaAsync(int mediaId, int releaseId)
    {
        try
        {
            await ValidateMediaAsync(mediaId);
            var toDelete = _context.Releases
                .Where(r => r.MediaId == mediaId)
                .FirstOrDefault(r => r.ReleaseId == releaseId)
                ?? throw new Exception("No release with id " + releaseId + " was found");
            
            _context.Releases.Remove(toDelete);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            throw new InvalidOperationException(e.Message);
        }
    }

    public async Task<Release> CreateReleaseForMediaAsync(int mediaId, Release release)
    {
        try
        {
            await ValidateMediaAsync(mediaId);
            var result = await _context.Releases.AddAsync(release);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
        catch (Exception e)
        {
            throw new InvalidOperationException(e.Message);
        }
    }

    public async Task<Release> UpdateReleaseForMediaAsync(int releaseId, Release release)
    {
        try
        {
            var existingRelease = await _context.Releases.FirstAsync(x => x.ReleaseId == releaseId);
            _context.Entry(existingRelease).CurrentValues.SetValues(release);
            
            await _context.SaveChangesAsync();
            return existingRelease;
        }
        catch (Exception e)
        {
            throw new InvalidOperationException(e.Message);
        }
    }

    public async Task<int> GetReleasesCountAsync(int mediaId)
    {
        return _context.Releases.Count(x => x.MediaId == mediaId);
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