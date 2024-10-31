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
    public IActionResult Get([FromQuery] MediaQueryParameter queryParameter)
    {
        switch (queryParameter.QueryType) {
            case MediaQueryType.Basic:
                return Ok(_mediaManager.GetAllMedia(queryParameter.Page));
            case MediaQueryType.ExactMatch:
                if (queryParameter.Keywords is null)
                    return BadRequest("Missing keyword query parameter.");
                return Ok(_mediaManager.SearchExactMatch(queryParameter.Keywords, queryParameter.Page));
        }

        return BadRequest();
    }
}