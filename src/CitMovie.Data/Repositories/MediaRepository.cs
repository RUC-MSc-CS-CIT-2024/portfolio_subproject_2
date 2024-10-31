namespace CitMovie.Data;

public class MediaRepository : IMediaRepository {
    private readonly DataContext _context;

    public MediaRepository(DataContext context)
    {
        _context = context;
    }

    public IEnumerable<Media> GetAllMedia(int page, int pageSize)
        => _context.Media
            .Include(x => x.PrimaryInformation)
                .ThenInclude(x => x.Title)
            .Include(x => x.PrimaryInformation)
                .ThenInclude(x => x.Release)
            .Include(x => x.PrimaryInformation)
                .ThenInclude(x => x.PromotionalMedia)
            .Skip(pageSize * page - 1)
            .Take(pageSize);

    public IEnumerable<Media> GetAllMedia(Func<Media, bool> wherePredicate, int page, int pageSize)
        => _context.Media
            .Include(x => x.PrimaryInformation)
                .ThenInclude(x => x.Title)
            .Include(x => x.PrimaryInformation)
                .ThenInclude(x => x.Release)
            .Include(x => x.PrimaryInformation)
                .ThenInclude(x => x.PromotionalMedia)
            .Where(wherePredicate)
            .Skip(pageSize * page - 1)
            .Take(pageSize);

    public Media? GetMediaById(int id)
        => _context.Media
            .Include(m => m.Genres)
            .Include(m => m.Scores)
            .Include(m => m.MediaProductionCompany)
            .Include(m => m.Countries)
            .Include(x => x.PrimaryInformation)
                .ThenInclude(x => x.Title)
            .Include(x => x.PrimaryInformation)
                .ThenInclude(x => x.Release)
            .Include(x => x.PrimaryInformation)
                .ThenInclude(x => x.PromotionalMedia)
            .FirstOrDefault(m => m.Id == id);

    public IEnumerable<Media> GetSimilarMedia(int id, int page, int pageSize)
    {
        IEnumerable<int> searchResult = _context
            .GetSimilarMedia(id)
            .Skip(pageSize * (page - 1))
            .Take(pageSize)
            .Select(x => x.Id)
            .Distinct()
            .ToList();

        return GetMediaFromIds(searchResult);
    }

    public IEnumerable<Media> SearchBestMatch(string[] keywords, int page, int pageSize) 
    {
        IEnumerable<int> searchResult = _context
            .BestMatchSearch(keywords)
            .Skip(pageSize * (page - 1))
            .Take(pageSize)
            .Select(x => x.Id)
            .Distinct()
            .ToList();
        
        return GetMediaFromIds(searchResult);
    }

    public IEnumerable<Media> SearchExactMatch(string[] keywords, int page, int pageSize) 
    {
        IEnumerable<int> searchResult = _context
            .ExactMatchSearch(keywords)
            .Skip(pageSize * (page - 1))
            .Take(pageSize)
            .Select(x => x.Id)
            .Distinct()
            .ToList();

        return GetMediaFromIds(searchResult);
    }
        
    public IEnumerable<Media> SearchSimple(string query, int userId, int page, int pageSize) 
    {
        IEnumerable<int> searchResult = _context
            .SimpleSearch(query, userId)
            .Skip(pageSize * (page - 1))
            .Take(pageSize)
            .Select(x => x.Id)
            .Distinct()
            .ToList();

        return GetMediaFromIds(searchResult);
    }

    public IEnumerable<Media> SearchStructured(string? title, string? plot, string? character, string? person, int userId, int page, int pageSize)
    {
        IEnumerable<int> searchResult = _context
            .StructuredSearch(title, plot, character, person, userId)
            .Skip(pageSize * (page - 1))
            .Take(pageSize)
            .Select(x => x.Id)
            .Distinct()
            .ToList();

        return GetMediaFromIds(searchResult);
    }

    private IQueryable<Media> GetMediaFromIds(IEnumerable<int> ids)
        => _context.Media
            .Where(x => ids.Contains(x.Id))
            .Include(x => x.PrimaryInformation)
                .ThenInclude(x => x.Title)
            .Include(x => x.PrimaryInformation)
                .ThenInclude(x => x.Release)
            .Include(x => x.PrimaryInformation)
                .ThenInclude(x => x.PromotionalMedia);
}
