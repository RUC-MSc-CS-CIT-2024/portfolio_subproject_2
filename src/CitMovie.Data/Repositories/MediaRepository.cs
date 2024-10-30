using Microsoft.EntityFrameworkCore;

namespace CitMovie.Data;

public class MediaRepository : IMediaRepository {
    private readonly DataContext _context;

    public MediaRepository(DataContext context)
    {
        _context = context;
    }

    IEnumerable<Media> IMediaRepository.GetAllMedia()
    {
        return _context.Media
            .Where(x => x.Type == "tvSeason")
            .Take(10);
    }
}