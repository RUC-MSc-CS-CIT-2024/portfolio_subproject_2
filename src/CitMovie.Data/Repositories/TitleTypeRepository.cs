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
            .Pagination(page, pageSize)
            .ToListAsync();
    }

    public async Task<TitleType> GetTitleTypeByIdAsync(int TitleTypeId)
    {
        return await _context.TitleTypes
            .FirstAsync(t => t.TitleTypeId == TitleTypeId);
    }

    public async Task<int> GetTotalTitleTypesCountAsync()
    {
        return await _context.TitleTypes.CountAsync();
    }

    public Task<List<TitleType>> GetMultipleByIdsAsync(IEnumerable<int> ids)
        => _context.TitleTypes
            .Where(x => ids.Contains(x.TitleTypeId))
            .ToListAsync();
}