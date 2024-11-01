using System.Text.Json;
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
        if (queryParameter.QueryType == MediaQueryType.Basic) 
            return Ok(_mediaManager.GetAllMedia(queryParameter.Page));

        string? userIdString = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
        int? userId = null;
        if (int.TryParse(userIdString, out int parseResult))
            userId = parseResult;

        return Ok(_mediaManager.Search(queryParameter, userId));
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id) {
        throw new NotImplementedException();
    }
}
