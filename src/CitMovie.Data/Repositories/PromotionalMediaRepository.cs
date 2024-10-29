using Microsoft.EntityFrameworkCore;

namespace CitMovie.Data;

public class PromotionalMediaRepository : IPromotionalMediaRepository
{
    private readonly DataContext _context;
    
    public PromotionalMediaRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<IList<PromotionalMedia>> GetPromotionalMediaAsync(int page, int pageSize)
    {
        return await _context.PromotionalMedia
            .Skip(pageSize * page)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<PromotionalMedia> GetPromotionalMediaByIdAsync(int id)
    {
        return await _context.PromotionalMedia.FirstAsync(p => p.PromotionalMediaId == id);
    }

    public async Task<bool> DeletePromotionalMediaAsync(int id)
    {
        try {
            PromotionalMedia PromotionalMediaToDelete = _context.PromotionalMedia
                .First(x => x.PromotionalMediaId == id);
            
            _context.PromotionalMedia.Remove(PromotionalMediaToDelete);
            await _context.SaveChangesAsync();
            return true;
        } catch {
            return false;
        }
    }
   public async Task<PromotionalMedia> CreatePromotionalMediaAsync(PromotionalMedia model)
   {
       _context.PromotionalMedia.Add(model);
       await _context.SaveChangesAsync();
       return model;
   }

    public async Task<int> GetPromotionalMediaCountAsync()
    {
        return await _context.PromotionalMedia.CountAsync();
    }
}