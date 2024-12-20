namespace CitMovie.Data;

public class PromotionalMediaRepository : IPromotionalMediaRepository
{
    private readonly DataContext _context;
    
    public PromotionalMediaRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PromotionalMedia>> GetPromotionalMediaOfMediaAsync(int mediaId, int page, int pageSize )
    {
        await ReleaseIdAndMediaIdValidator(mediaId, null);
        return await _context.PromotionalMedia
            .AsNoTracking()
            .Include(pm => pm.Release)
            .Where(pm => pm.Release!.MediaId == mediaId)
            .OrderBy(pm => pm.PromotionalMediaId)
            .Pagination(page, pageSize)
            .ToListAsync();
    }

    public async Task<IList<PromotionalMedia>> GetPromotionalMediaOfReleaseAsync(int mediaId, int releaseId, int page, int pageSize)
    {
        await ReleaseIdAndMediaIdValidator(mediaId, releaseId);
        return await _context.PromotionalMedia
            .Include(pm => pm.Release)
            .Where(pm => pm.ReleaseId == releaseId && pm.Release!.MediaId == mediaId)
            .OrderBy(pm => pm.PromotionalMediaId)
            .Pagination(page, pageSize)
            .ToListAsync();
    }

    public async Task<PromotionalMedia> GetPromotionalMediaByIdAsync(int id, int? mediaId, int? releaseId)
    {
        if (!mediaId.HasValue && !releaseId.HasValue)
        {
            return await _context.PromotionalMedia
                .Include(pm => pm.Release)
                .FirstAsync(p => p.PromotionalMediaId == id);
        }
        
        return await _context.PromotionalMedia
            .Include(pm => pm.Release)
            .FirstAsync(pm => pm.PromotionalMediaId == id 
                && pm.ReleaseId == releaseId 
                && pm.Release!.MediaId == mediaId);
        
    }

    public async Task<bool> DeletePromotionalMediaAsync(int mediaId, int releaseId, int id)
    {
        try {
            await ReleaseIdAndMediaIdValidator(mediaId, releaseId);
            PromotionalMedia promotionalMediaToDelete = _context.PromotionalMedia
                .Include(pm => pm.Release)
                .First(pm => pm.PromotionalMediaId == id 
                    && pm.ReleaseId == releaseId 
                    && pm.Release!.MediaId == mediaId);
            
            _context.PromotionalMedia.Remove(promotionalMediaToDelete);
            await _context.SaveChangesAsync();
            return true;
        } catch {
            return false;
        }
    }
    
   public async Task<PromotionalMedia> CreatePromotionalMediaAsync(int mediaId, int releaseId,PromotionalMedia model)
   {
       await ReleaseIdAndMediaIdValidator(mediaId, releaseId);
       var result = await _context.PromotionalMedia.AddAsync(model);
       await _context.SaveChangesAsync();
       return result.Entity;
   }
   

    public async Task<int> GetPromotionalMediaCountAsync(int id, string parameter)
    { 
        return parameter switch {
            "release" => await _context.PromotionalMedia
                                        .CountAsync(r => r.ReleaseId == id),
            "media" => await _context.PromotionalMedia
                                        .Include(x => x.Release)
                                        .CountAsync(r => r.Release!.MediaId == id),
            _ => throw new ArgumentException("Invalid parameter", nameof(parameter))
        };
    }

    private async Task ReleaseIdAndMediaIdValidator(int mediaId, int? releaseId)
    {
        var checkPassed = releaseId.HasValue 
            ? await _context.Releases.AnyAsync(pm => pm.MediaId == mediaId && pm.ReleaseId == releaseId)
            : await _context.Releases.AnyAsync(pm => pm.MediaId == mediaId);
        if (!checkPassed)
        {
            throw new Exception($"Media or release do not exists or check if media id and release id are correct.");
        }
    }
}