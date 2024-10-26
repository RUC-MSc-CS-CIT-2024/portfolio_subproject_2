using System.Text;

namespace CitMovie.Api;

[ApiController]
[Route("api")]
public class LoginController : ControllerBase {
    private readonly ILogger<LoginController> _logger;
    private readonly ILoginManager _loginService;

    public LoginController(ILogger<LoginController> logger, ILoginManager loginService)
    {
        _logger = logger;
        _loginService = loginService;
    }
    
    [HttpPost("login"), RequireHttps]
    public ActionResult Login([FromHeader(Name = "Authorization")] string? authorizationHeader) {
        if (authorizationHeader == null) {
            return Unauthorized("Authorization header is missing");
        }
        string[] authStatement = authorizationHeader.Split(' ');
        if (authStatement.Length != 2) {
            return Unauthorized("Authorization header is invalid");
        }

        string authType = authStatement[0];
        string authValue = authStatement[1];

        if (authType != "Basic") {
            return Unauthorized("Authorization header is invalid");
        }

        string[] credentialParts = Encoding.UTF8.GetString(Convert.FromBase64String(authValue)).Split(':');
        if (credentialParts.Length != 2) {
            return Unauthorized("Authorization header is invalid");
        }

        string username = credentialParts[0];
        string password = credentialParts[1];

        _logger.LogInformation($"Login attempt with username: {username} and password: {password}");

        try {
            string token = _loginService.Login(username, password);
            return Ok(token);
        } catch (Exception ex) {
            _logger.LogInformation(ex, "Login failed");
            return Unauthorized("Invalid username or password");
        }
    }
}
