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
        await ValidateMediaAsync(mediaId);
        return await _context.Releases
            .Include(x => x.Country)
            .Include(x => x.SpokenLanguages)
            .Where(r => r.MediaId == mediaId)
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<Release> GetReleaseOfMediaByIdAsync(int mediaId, int releaseId)
    {
        await ValidateMediaAsync(mediaId);
        var result = await _context.Releases
            .Include(x => x.Country)
            .Include(x => x.SpokenLanguages)
            .Where(r => r.MediaId == mediaId)
            .FirstAsync(r => r.ReleaseId == releaseId);
        
        return result;
    }

    public async Task<bool> DeleteReleaseOfMediaAsync(int mediaId, int releaseId)
    {
        await ValidateMediaAsync(mediaId);
        var toDelete = await GetReleaseByIdAsync(releaseId);
        _context.Releases.Remove(toDelete);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Release> CreateReleaseForMediaAsync(int mediaId, Release release)
    {
        await ValidateMediaAsync(mediaId);

        var result = await _context.Releases.AddAsync(release);
        await _context.SaveChangesAsync();
        return await GetReleaseByIdAsync(result.Entity.ReleaseId);
    }

    public async Task<Release> UpdateReleaseForMediaAsync(int mediaId, int releaseId, Release release)
    {
        await ValidateMediaAsync(mediaId);

        var existingRelease = await GetReleaseByIdAsync(releaseId);
        
        _context.Entry(existingRelease).CurrentValues.SetValues(release);
        
        existingRelease.SpokenLanguages.Clear();
        
        foreach (var language in release.SpokenLanguages)
        {
            existingRelease.SpokenLanguages.Add(language);
        }

        await _context.SaveChangesAsync();
        return await GetReleaseByIdAsync(releaseId);
    }

    public async Task<Release> GetReleaseByIdAsync(int releaseId)
    {
        return await _context.Releases
            .Include(x => x.Title)
            .Include(x => x.Country)
            .Include(x => x.SpokenLanguages)
            .FirstAsync(x => x.ReleaseId == releaseId);;
    }

    public async Task<int> GetReleasesCountAsync(int mediaId)
    {
        return _context.Releases.Count(x => x.MediaId == mediaId);
    }

    private async Task ValidateMediaAsync(int mediaId)
    {
        await _context.Media.FirstAsync(m => m.Id == mediaId);
    }
}