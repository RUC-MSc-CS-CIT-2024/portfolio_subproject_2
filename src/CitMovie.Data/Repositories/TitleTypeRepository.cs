using Microsoft.EntityFrameworkCore;

namespace CitMovie.Data;

public class TitleTypeRepository : ITitleTypeRepository
{
    private readonly DataContext _context;

    public TitleTypeRepository(DataContext context)
    {
        _context = context;
    }
    
    public async Task<List<TitleType>> GetTitleTypesAsync(int page, int pageSize)
    {
        return await _context.TitleTypes
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<TitleType?> GetTitleTypeByIdAsync(int TitleTypeId)
    {
        return await _context.TitleTypes
            .FirstAsync(t => t.TitleTypeId == TitleTypeId);
    }

    public Task<TitleType> AddTitleTypeAsync(TitleType titleType)
    {
        throw new NotImplementedException();
    }

    public Task<TitleType> UpdateTitleTypeAsync(TitleType titleType)
    {
        throw new NotImplementedException();
    }

    public Task<TitleType> DeleteTitleTypeAsync(TitleType titleType)
    {
        throw new NotImplementedException();
    }

    public async Task<int> GetTotalTitleTypesCountAsync()
    {
        return await _context.TitleTypes.CountAsync();
    }
}