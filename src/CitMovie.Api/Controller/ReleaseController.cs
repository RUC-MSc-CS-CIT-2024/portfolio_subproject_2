using CitMovie.Models;

namespace CitMovie.Api;
[ApiController]
[Route("/api/media/{mediaId}/releases")]
[Tags("Media")]
public class ReleaseController : ControllerBase
{
    private readonly IReleaseManager _releaseManager;
    private readonly IMediaManager _mediaManager;
    private readonly PagingHelper _pageHelper;

    public ReleaseController(IReleaseManager releaseManager, IMediaManager mediaManager, PagingHelper pagingHelper)
    {
        _releaseManager = releaseManager;
        _mediaManager = mediaManager;
        _pageHelper = pagingHelper;
    }

    [HttpGet(Name = nameof(GetReleasesOfMedia))]
    public async Task<IActionResult> GetReleasesOfMedia(int mediaId, [FromQuery] PageQueryParameter page)
    {
        try
        {
            var releases = await _releaseManager.GetReleasesOfMediaAsync(mediaId, page.Number, page.Count);
            var totalItems = await _releaseManager.GetReleasesCountAsync(mediaId);

            foreach (var release in releases)
            {
                AddReleaseLinks(release);
            }

            var result = _pageHelper.CreatePaging(nameof(GetReleasesOfMedia), page.Number, page.Count, totalItems, releases, new { mediaId });

            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{id}", Name = nameof(GetReleaseOfMediaById))]
    public async Task<IActionResult> GetReleaseOfMediaById(int mediaId, int id)
    {
        try
        {
            var release = await _releaseManager.GetReleaseOfMediaByIdAsync(mediaId, id);

            if (release == null)
            {
                return NotFound();
            }
            AddReleaseLinks(release);

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

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateReleaseForMediaAsync(int mediaId, int id, ReleaseUpdateRequest request)
    {
        try
        {
            var result = await _releaseManager.UpdateReleaseForMediaAsync(mediaId, id, request);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    private void AddReleaseLinks(ReleaseResult release)
    {
        release.Links.Add(new Link
        {
            Href = _pageHelper.GetResourceLink(nameof(GetReleaseOfMediaById), new { mediaId = release.MediaId, id = release.ReleaseId }) ?? string.Empty,
            Rel = "self",
            Method = "GET"
        });

        var media = _mediaManager.Get(release.MediaId);
        if (media != null)
        {
            release.Links.Add(new Link
            {
                Href = _pageHelper.GetResourceLink(nameof(MediaController.Get), new { id = media.Id }) ?? string.Empty,
                Rel = "media",
                Method = "GET"
            });
        }
    }
}