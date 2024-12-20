namespace CitMovie.Data;

public class TitleAttributeRepository : ITitleAttributeRepository
{
    private readonly DataContext _context;
    
    public TitleAttributeRepository(DataContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<TitleAttribute>> GetTitleAttributesAsync(int page, int pageSize)
    {
        return await _context.TitleAttributes
            .Include(x => x.Titles)
            .OrderBy(x => x.Name)
            .ThenBy(x => x.TitleAttributeId)
            .Pagination(page, pageSize)
            .ToListAsync();
    }
    
    public async Task<TitleAttribute> GetTitleAttributeByIdAsync(int id)
    {
        return await _context.TitleAttributes
            .Include(x => x.Titles)
            .FirstAsync(x => x.TitleAttributeId == id);
    }
    
    public async Task<int> GetTotalTitleAttributesCountAsync()
    {
        return await _context.TitleAttributes.CountAsync();
    }

    public Task<List<TitleAttribute>> GetMultipleByIdsAsync(IEnumerable<int> ids)
        => _context.TitleAttributes
            .Where(x => ids.Contains(x.TitleAttributeId))
            .ToListAsync();
}