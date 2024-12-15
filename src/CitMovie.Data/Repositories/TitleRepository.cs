namespace CitMovie.Data;

public class TitleRepository : ITitleRepository {
    private readonly DataContext _context;

    public TitleRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<Title> CreateAsync(Title title)
    {
        _context.Titles.Add(title);
        await _context.SaveChangesAsync();
        return _context.Titles
            .AsNoTracking()
            .Include(x => x.TitleAttributes)
            .Include(x => x.TitleTypes)
            .First(x => x.Id == title.Id);
    }

    public async Task<bool> DeleteAsync(int titleId)
    {
        try {
            Title titleToDelete = _context.Titles.First(x => x.Id == titleId);
            _context.Titles.Remove(titleToDelete);
            await _context.SaveChangesAsync();
            return true;
        } catch {
            return false;
        }
    }

    public Title Get(int titleId)
        => _context.Titles
            .AsNoTracking()
            .Include(x => x.TitleTypes)
            .Include(x => x.TitleAttributes)
            .First(x => x.Id == titleId);

    public IEnumerable<Title> GetForMedia(int mediaId, int page, int count)
        => _context.Titles
            .AsNoTracking()
            .Include(x => x.TitleAttributes)
            .Include(x => x.TitleTypes)
            .Where(x => x.MediaId == mediaId)
            .OrderBy(x => x.Name)
            .ThenBy(x => x.Id)
            .Pagination(page, count)
            .ToList();

    public int GetTotalTitles(int mediaId)
        => _context.Titles
            .Count(x => x.MediaId == mediaId);

}
