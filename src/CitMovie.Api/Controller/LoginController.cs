using System.Text;

namespace CitMovie.Api;

[ApiController]
[Route("api/auth")]
[Tags("Authentication and connection")]
public class LoginController : ControllerBase {
    private readonly ILogger<LoginController> _logger;
    private readonly ILoginManager _loginService;

    public LoginController(ILogger<LoginController> logger, ILoginManager loginService)
    {
        _logger = logger;
        _loginService = loginService;
    }
    
    [HttpPost("login"), RequireHttps]
    public async Task<ActionResult> Login([FromHeader(Name = "Authorization")] string? authorizationHeader) {
        if (authorizationHeader == null) {
            return BadRequest("Authorization header is missing");
        }
        string[] authStatement = authorizationHeader.Split(' ');
        if (authStatement.Length != 2) {
            return BadRequest("Authorization header is invalid");
        }

        string authType = authStatement[0];
        string authValue = authStatement[1];

        if (authType != "Basic") {
            return BadRequest("Authorization header is invalid");
        }

        string[] credentialParts = Encoding.UTF8.GetString(Convert.FromBase64String(authValue)).Split(':');
        if (credentialParts.Length != 2) {
            return BadRequest("Authorization header is invalid");
        }

        string username = credentialParts[0];
        string password = credentialParts[1];

        try {
            string token = await _loginService.LoginAsync(username, password);
            return Ok(token);
        } catch (Exception ex) {
            _logger.LogInformation(ex, "Login failed");
            return Unauthorized("Invalid username or password");
        }
    }
}
