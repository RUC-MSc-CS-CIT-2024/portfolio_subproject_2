namespace CitMovie.Data;

public class CrewRepository : ICrewRepository
{
    private readonly DataContext _context;

    public CrewRepository(DataContext context)
    {
        _context = context;
    }
    public Task<List<CrewMember>> GetCrewAsync(int mediaId, int page, int pageCount) 
        => _context.CrewMembers
            .AsNoTracking()
            .Where(x => x.MediaId == mediaId)
            .Include(x => x.Person)
            .OrderBy(x => x.Person!.Name)
            .ThenBy(x => x.PersonId)
            .Pagination(page, pageCount)
            .Include(x => x.JobCategory)
            .ToListAsync();
    
    public Task<List<CastMember>> GetCastAsync(int mediaId, int page, int pageCount) 
        => _context.CastMembers
            .AsNoTracking()
            .Where(x => x.MediaId == mediaId)
            .Include(x => x.Person)
            .OrderBy(x => x.Person!.Name)
            .ThenBy(x => x.PersonId)
            .Pagination(page, pageCount)
            .ToListAsync();

    public Task<int> GetTotalCrewCountAsync(int id)
        => _context.CrewMembers.CountAsync(x => x.MediaId == id);

    public Task<int> GetTotalCastCountAsync(int id)
        => _context.CastMembers.CountAsync(x => x.MediaId == id);

}
