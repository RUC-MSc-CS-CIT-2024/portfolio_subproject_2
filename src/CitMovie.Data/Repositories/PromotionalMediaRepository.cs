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

    public async Task<int> GetPromotionalMediaCountAsync()
    {
        return await _context.PromotionalMedia.CountAsync();
    }
}