using Microsoft.EntityFrameworkCore;

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
        try
        {
            return await _context.PromotionalMedia
                .Include(pm => pm.Release)
                .Where(pm => pm.Release.MediaId == mediaId)
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        catch
        {
            throw new InvalidOperationException("There was an error retrieving the promotional media. Check if relese id is correct.");
        }

    }

    public async Task<IList<PromotionalMedia>> GetPromotionalMediaOfReleaseAsync(int mediaId, int releaseId, int page, int pageSize)
    {
        try
        {
            return await _context.PromotionalMedia
                .Include(pm => pm.Release)
                .Where(pm => pm.ReleaseId == releaseId)
                .Where(pm => pm.Release.MediaId == mediaId)
                .Skip(pageSize * page)
                .Take(pageSize)
                .ToListAsync();
        }
        catch
        {
            throw new InvalidOperationException("There was an error retrieving the promotional media. Check if release id and media id are correct.");
        }

    }

    public async Task<PromotionalMedia> GetPromotionalMediaByIdAsync(int id, int? mediaId, int? releaseId)
    {
        if (!mediaId.HasValue && !releaseId.HasValue)
        {
            try
            {
                return await _context.PromotionalMedia
                    .Include(pm => pm.Release)
                    .FirstAsync(p => p.PromotionalMediaId == id);
            }
            catch { throw new InvalidOperationException("Promotional media not found"); }
        }

        try
        {
            return await _context.PromotionalMedia
                .Include(pm => pm.Release)
                .Where(pm => pm.ReleaseId == releaseId)
                .Where(pm => pm.Release.MediaId == mediaId)
                .FirstAsync(p => p.PromotionalMediaId == id);
        }
        catch { throw new InvalidOperationException("Promotional media not found, check if mediaId and releaseId values are correct." +
                                                    "Otherwise promotional media not found"); }

        
    }

    public async Task<bool> DeletePromotionalMediaAsync(int mediaId, int releaseid, int id)
    {
        try {
            PromotionalMedia PromotionalMediaToDelete = _context.PromotionalMedia
                .Include(pm => pm.Release)
                .Where(pm => pm.ReleaseId == releaseid)
                .Where(pm => pm.Release.MediaId == mediaId)
                .First(x => x.PromotionalMediaId == id);
            
            _context.PromotionalMedia.Remove(PromotionalMediaToDelete);
            await _context.SaveChangesAsync();
            return true;
        } catch {
            return false;
        }
    }
    
   public async Task<PromotionalMedia> CreatePromotionalMediaAsync(int mediaId, int releaseId,PromotionalMedia model)
   {
       try
       {
           var exists = _context.Releases.Any(pm => pm.MediaId == mediaId && pm.ReleaseId == releaseId);

           if (!exists)
           {
               throw new Exception($"Invalid data: Release with id {releaseId} does not belong to Media with id {mediaId}");
           }
           
           _context.PromotionalMedia.Add(model);
           await _context.SaveChangesAsync();
           return model;
       }
       catch (Exception e)
       {
           throw new InvalidOperationException(e.Message);
       }
   }
   

    public async Task<int> GetPromotionalMediaCountAsync(int id, string parameter)
    {
        if (parameter == "release")
        {
            return await _context.PromotionalMedia.CountAsync(r => r.ReleaseId == id);
        }

        if (parameter == "media")
        {
            return await _context.PromotionalMedia.CountAsync(r => r.Release.MediaId == id);
        }
        
        throw new ArgumentException("Invalid parameter");
    }
}