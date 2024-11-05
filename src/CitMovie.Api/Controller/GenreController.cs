namespace CitMovie.Api;

[ApiController]
[Route("api/genres")]
[Tags("Base data")]
public class GenreController : ControllerBase
{
    private readonly IGenreManager _genreManager;
    private readonly PagingHelper _pagingHelper;

    public GenreController(IGenreManager genreManager, PagingHelper pagingHelper)
    {
        _genreManager = genreManager;
        _pagingHelper = pagingHelper;
    }

    [HttpGet(Name = nameof(GetAllGenres))]
    public async Task<ActionResult<IEnumerable<GenreResult>>> GetAllGenres(int page = 0, int pageSize = 10)
    {
        var totalItems = await _genreManager.GetTotalGenresCountAsync();
        var genres = await _genreManager.GetAllGenresAsync(page, pageSize);

        var result = _pagingHelper.CreatePaging(nameof(GetAllGenres), page, pageSize, totalItems, genres);

        return Ok(result);
    }
}
