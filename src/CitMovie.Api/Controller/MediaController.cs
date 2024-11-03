using CitMovie.Models.DomainObjects;

namespace CitMovie.Api;

[ApiController]
[Route("api/media")]
public class MediaController : ControllerBase
{
    private readonly IMediaManager _mediaManager;

    public MediaController(IMediaManager mediaManager)
    {
        _mediaManager = mediaManager;
    }

    [HttpGet]
    public IEnumerable<Media> Get()
    {
        return _mediaManager.GetAllMedia();
    }
}