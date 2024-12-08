namespace CitMovie.Data;

public class CrewRepository : ICrewRepository
{
    private readonly DataContext _context;

    public CrewRepository(DataContext context)
    {
        _context = context;
    }
    public Task<List<CrewMember>> GetCrewAsync(int mediaId, int page, int pageCount) 
        => _context.Media
            .AsNoTracking()
            .Include(x => x.CrewMembers)
            .Where(x => x.Id == mediaId)
            .SelectMany(x => x.CrewMembers)
            .Include(x => x.Person)
            .Include(x => x.JobCategory)
            .OrderBy(x => x.PersonId)
            .ThenBy(x => x.Person!.Name)
            .Pagination(page, pageCount)
            .ToListAsync();
    
    public Task<List<CastMember>> GetCastAsync(int mediaId, int page, int pageCount) 
        => _context.Media
            .AsNoTracking()
            .Include(x => x.CastMembers)
            .Where(x => x.Id == mediaId)
            .SelectMany(x => x.CastMembers)
            .Include(x => x.Person)
            .OrderBy(x => x.PersonId)
            .ThenBy(x => x.Person!.Name)
            .Pagination(page, pageCount)
            .ToListAsync();

    public Task<int> GetTotalCrewCountAsync(int id)
        => _context.Media
            .AsNoTracking()
            .Include(x => x.CrewMembers)
            .Where(x => x.Id == id)
            .SelectMany(x => x.CrewMembers)
            .CountAsync();

    public Task<int> GetTotalCastCountAsync(int id)
        => _context.Media
            .AsNoTracking()
            .Include(x => x.CastMembers)
            .Where(x => x.Id == id)
            .SelectMany(x => x.CastMembers)
            .CountAsync();

}
