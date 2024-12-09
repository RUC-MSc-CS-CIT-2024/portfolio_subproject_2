namespace CitMovie.Data;

public class MediaRepository : IMediaRepository
{
    private readonly DataContext _context;

    public MediaRepository(DataContext context)
    {
        _context = context;
    }

    public Media? Get(int id)
        => _context.Media.FirstOrDefault();

    public IEnumerable<Media> GetAll(int page, int pageSize)
        => GetMultipleWithInclude()
            .OrderBy(x => x.Id)
            .ThenBy(x => x.PrimaryInformation!.Title!.Name)
            .Pagination(page, pageSize)
            .ToList();

    public Media? GetDetailed(int id)
        => _context.Media
            .AsNoTracking()
            .Include(m => m.Genres)
            .Include(m => m.Scores)
            .Include(m => m.MediaProductionCompany)
            .Include(m => m.Countries)
            .Include(x => x.PrimaryInformation!)
            .ThenInclude(x => x.Title)
            .Include(x => x.PrimaryInformation!)
            .ThenInclude(x => x.Release)
            .Include(x => x.PrimaryInformation!)
            .ThenInclude(x => x.PromotionalMedia)
            .FirstOrDefault(m => m.Id == id);

    public IEnumerable<Media> GetRelated(int id, int page, int pageSize)
    {
        IQueryable<Media> related = _context.Media
            .Include(x => x.RelatedMedia)
            .ThenInclude(x => x.Related)
            .Where(x => x.Id == id)
            .SelectMany(x => x.RelatedMedia.Select(y => y.Related!));

        IQueryable<Media> withIncludes = GetMultipleWithInclude(related);

        return withIncludes
            .OrderBy(x => x.Id)
            .ThenBy(x => x.PrimaryInformation!.Title!.Name)
            .Pagination(page, pageSize)
            .ToList();
    }

    public IEnumerable<Media> GetSimilar(int id, int page, int pageSize)
    {
        IQueryable<MediaSearchResult> searchResult = _context.GetSimilarMedia(id);
        return GetMediaFromSearchQuery(searchResult, page, pageSize);
    }

    public IEnumerable<Media> SearchBestMatch(string[] keywords, int? userId, int page, int pageSize)
    {
        IQueryable<MediaSearchResult> searchResult = _context.BestMatchSearch(keywords, userId);
        return GetMediaFromSearchQuery(searchResult, page, pageSize);
    }

    public IEnumerable<Media> SearchExactMatch(string[] keywords, int? userId, int page, int pageSize)
    {
        IQueryable<MediaSearchResult> searchResult = _context.ExactMatchSearch(keywords, userId);
        return GetMediaFromSearchQuery(searchResult, page, pageSize);
    }

    public IEnumerable<Media> SearchSimple(string query, int? userId, int page, int pageSize)
    {
        IQueryable<MediaSearchResult> searchResult = _context.SimpleSearch(query, userId);
        return GetMediaFromSearchQuery(searchResult, page, pageSize);
    }

    public IEnumerable<Media> SearchStructured(string? title, string? plot, string? character, string? person,
        int? userId, int page, int pageSize)
    {
        IQueryable<MediaSearchResult> searchResult = _context.StructuredSearch(title, plot, character, person, userId);
        return GetMediaFromSearchQuery(searchResult, page, pageSize);
    }

    private IEnumerable<Media> GetMediaFromSearchQuery(IQueryable<MediaSearchResult> query, int page, int pageSize)
    {
        IQueryable<int> ids = query
            .Pagination(page, pageSize)
            .Select(x => x.Id)
            .Distinct();

        return GetMultipleWithInclude()
            .Where(x => ids.Contains(x.Id))
            .ToList();
    }
    
    private IEnumerable<Media> GetMediaFromSearchQuery(IQueryable<MediaSearchResult> query)
    {
        IQueryable<int> ids = query
            .OrderBy(x => x.Id)
            .ThenBy(x => x.Title)
            .Select(x => x.Id)
            .Distinct();

        return GetMultipleWithInclude()
            .Where(x => ids.Contains(x.Id))
            .ToList();
    }

    private IQueryable<Media> GetMultipleWithInclude()
        => GetMultipleWithInclude(_context.Media);

    private IQueryable<Media> GetMultipleWithInclude(IQueryable<Media> media)
        => media
            .AsNoTracking()
            .Include(x => x.PrimaryInformation!)
            .ThenInclude(x => x.Title)
            .Include(x => x.PrimaryInformation!)
            .ThenInclude(x => x.Release)
            .Include(x => x.PrimaryInformation!)
            .ThenInclude(x => x.PromotionalMedia);


    public async Task<int> GetTotalRelatedMediaCountAsync(int id)
        => await _context.Media
            .Include(x => x.RelatedMedia)
            .Where(x => x.Id == id)
            .SelectMany(x => x.RelatedMedia.Select(y => y.Related))
            .CountAsync();

    public async Task<int> GetTotalSimilarMediaCountAsync(int id)
        => await _context.GetSimilarMedia(id).CountAsync();
    
    public int GetSimpleSearchResultsCount(string query)
    {
        IQueryable<MediaSearchResult> searchResults = _context.SimpleSearch(query, null);
        return GetMediaFromSearchQuery(searchResults).Count();
    }
    
    public int GetBestMatchSearchResultsCount(string[] keywords)
    {
        IQueryable<MediaSearchResult> searchResults = _context.BestMatchSearch(keywords, null);
        return GetMediaFromSearchQuery(searchResults).Count();
    }
    
    public int GetExactMatchSearchResultsCount(string[] keywords)
    {
        IQueryable<MediaSearchResult> searchResults = _context.ExactMatchSearch(keywords, null);
        return GetMediaFromSearchQuery(searchResults).Count();
    }
    
    public int GetStructuredSearchResultsCount(string? title, string? plot, string? character, string? person)
    {
        IQueryable<MediaSearchResult> searchResults = _context.StructuredSearch(title, plot, character, person, null);
        return GetMediaFromSearchQuery(searchResults).Count();
    }
    
    public int GetTotalMediaCount()
        => _context.Media.Count();
}