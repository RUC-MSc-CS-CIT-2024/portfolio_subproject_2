using System.Text.Json;

namespace CitMovie.Data;

public class TitleRepository : ITitleRepository {
    private readonly DataContext _context;

    public TitleRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<Title> CreateAsync(Title title)
    {
        Console.WriteLine(JsonSerializer.Serialize(title));
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

    public IEnumerable<Title> GetForMedia(int mediaId)
        => _context.Titles
            .AsNoTracking()
            .Include(x => x.TitleAttributes)
            .Include(x => x.TitleTypes)
            .Where(x => x.MediaId == mediaId);
}
