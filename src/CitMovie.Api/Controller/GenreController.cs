using CitMovie.Business.Managers;
using CitMovie.Models.DTOs;

namespace CitMovie.Api.Controller
{
    [ApiController]
    [Route("api/genres")]
    public class GenreController : ControllerBase
    {
        private readonly GenreManager _genreService;
        private readonly LinkGenerator _linkGenerator;

        public GenreController(GenreManager genreService, LinkGenerator linkGenerator)
        {
            _genreService = genreService;
            _linkGenerator = linkGenerator;
        }

        [HttpGet(Name = nameof(GetAllGenres))]
        public async Task<ActionResult<IEnumerable<GenreDto>>> GetAllGenres(int page = 0, int pageSize = 10)
        {
            var totalItems = await _genreService.GetTotalGenresCountAsync();
            var genres = await _genreService.GetAllGenresAsync(page, pageSize);

            var result = CreatePaging(
                nameof(GetAllGenres),
                page,
                pageSize,
                totalItems,
                genres
            );

            return Ok(result);
        }

        // HATEOAS and Pagination
        private string? GetLink(string linkName, int page, int pageSize)
        {
            var uri = _linkGenerator.GetUriByName(
                        HttpContext,
                        linkName,
                        new { page, pageSize }
                        );
            return uri;
        }

        private object CreatePaging<T>(string linkName, int page, int pageSize, int total, IEnumerable<T?> items)
        {
            var numberOfPages = (int)Math.Ceiling(total / (double)pageSize);

            var curPage = GetLink(linkName, page, pageSize);

            var nextPage = page < numberOfPages - 1
                ? GetLink(linkName, page + 1, pageSize)
                : null;

            var prevPage = page > 0
                ? GetLink(linkName, page - 1, pageSize)
                : null;

            var result = new
            {
                CurPage = curPage,
                NextPage = nextPage,
                PrevPage = prevPage,
                NumberOfItems = total,
                NumberPages = numberOfPages,
                Items = items
            };
            return result;
        }
    }
}