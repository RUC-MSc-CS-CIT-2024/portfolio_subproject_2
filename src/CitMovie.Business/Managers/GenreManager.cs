namespace CitMovie.Business;

public class GenreManager : IGenreManager
{
    private readonly IGenreRepository _genreRepository;
    private readonly IMapper _mapper;

    public GenreManager(IGenreRepository genreRepository, IMapper mapper)
    {
        _genreRepository = genreRepository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<GenreResult>> GetAllGenresAsync(int page, int pageSize)
    {
        var genres = await _genreRepository.GetAllGenresAsync(page, pageSize);
        return _mapper.Map<IEnumerable<GenreResult>>(genres);
    }

    public async Task<int> GetTotalGenresCountAsync()
    {
        return await _genreRepository.GetTotalGenresCountAsync();
    }
}
