namespace CitMovie.Api;
[ApiController]
[Route("/api/media/{mediaId}/releases")]
public class ReleaseController : ControllerBase
{
    private readonly IReleaseManager _releaseManager;
    private readonly LinkGenerator _linkGenerator;

    public ReleaseController(IReleaseManager releaseManager, LinkGenerator linkGenerator)
    {
        _releaseManager = releaseManager;
        _linkGenerator = linkGenerator;
    }

    [HttpGet(Name = nameof(GetReleasesOfMediaAsync))]
    public async Task<IActionResult> GetReleasesOfMediaAsync(int mediaId, int page = 0, int pageSize = 10)
    {
        try
        {
            var releases = await _releaseManager.GetReleasesOfMediaAsync(mediaId, page, pageSize);
            var totalItems = await _releaseManager.GetReleasesCountAsync(mediaId);

            var result = CreatePaging(
                nameof(GetReleasesOfMediaAsync),
                mediaId,
                page,
                pageSize,
                totalItems,
                releases);
        
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{id}", Name = nameof(GetReleaseOfMediaByIdAsync))]
    public async Task<IActionResult> GetReleaseOfMediaByIdAsync(int mediaId, int id)
    {
        try
        {
            var release = await _releaseManager.GetReleaseOfMediaByIdAsync(mediaId, id);
            return Ok(release);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReleaseOfMediaAsync(int mediaId, int id)
    {
        try
        {
            var release = await _releaseManager.DeleteReleaseOfMediaAsync(mediaId, id);
            return Ok(release);

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateReleaseForMediaAsync(int mediaId, ReleaseCreateRequest request)
    {
        try
        {
            var result = await _releaseManager.CreateReleaseForMediaAsync(mediaId, request);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    private string? GetLink(string linkName,int mediaId, int page, int pageSize)
    {
        var uri = _linkGenerator.GetUriByName(
            HttpContext,
            linkName,
            new {mediaId, page, pageSize }
        );
        return uri;
    }

    private object CreatePaging<T>(string linkName, int mediaId, int page, int pageSize, int total, IEnumerable<T?> items)
    {
        var numberOfPages = (int)Math.Ceiling(total / (double)pageSize);

        var curPage = GetLink(linkName, mediaId, page, pageSize);

        var nextPage = page < numberOfPages - 1
            ? GetLink(linkName, mediaId, page + 1, pageSize)
            : null;

        var prevPage = page > 0
            ? GetLink(linkName, mediaId, page - 1, pageSize)
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