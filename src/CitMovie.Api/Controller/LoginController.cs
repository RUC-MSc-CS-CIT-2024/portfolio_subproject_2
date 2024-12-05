using System.Text;

namespace CitMovie.Api;

[ApiController]
[Route("api/auth")]
[Tags("Authentication")]
public class LoginController : ControllerBase {
    private readonly ILogger<LoginController> _logger;
    private readonly ILoginManager _loginService;

    public LoginController(ILogger<LoginController> logger, ILoginManager loginService)
    {
        _logger = logger;
        _loginService = loginService;
    }
    
    [HttpPost("generate-token"), RequireHttps]
    public async Task<ActionResult> Login([FromHeader(Name = "Authorization")] string? authorizationHeader) {
        if (authorizationHeader == null)
            return BadRequest("Authorization header is missing");

        string[] authStatement = authorizationHeader.Split(' ');
        if (authStatement.Length != 2)
            return BadRequest("Authorization header is invalid");

        string authType = authStatement[0];
        string authValue = authStatement[1];

        if (authType != "Basic")
            return BadRequest("Authorization header is invalid");

        string[] credentialParts = Encoding.UTF8.GetString(Convert.FromBase64String(authValue)).Split(':');
        if (credentialParts.Length != 2)
            return BadRequest("Authorization header is invalid");

        string username = credentialParts[0];
        string password = credentialParts[1];

        try {
            TokenDto token = await _loginService.AuthenticateAsync(username, password);
            Response.Cookies.Append("access-token", token.AccessToken, new () {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Lax,
                Expires = DateTimeOffset.UtcNow.AddSeconds(token.ExpiresIn)
            });
            return Ok(token);
        } catch (Exception ex) {
            _logger.LogInformation(ex, "Login failed");
            return Unauthorized("Invalid username or password");
        }
    }

    [HttpPost("refresh-token"), RequireHttps]
    public async Task<ActionResult> RefreshToken([FromQuery] string refreshToken) {
        try {
            TokenDto result = await _loginService.AuthenticateAsync(refreshToken);
            Response.Cookies.Append("access-token", result.AccessToken, new () {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Lax,
                Expires = DateTimeOffset.UtcNow.AddSeconds(result.ExpiresIn)
            });
            return Ok(refreshToken);
        } catch (Exception ex) {
            _logger.LogInformation(ex, "Login failed");
            return Unauthorized("Refresh token is invalid");
        }
    }

    [HttpPost("revoke-token"), RequireHttps]
    public ActionResult RevokeToken([FromBody] string refreshToken) {
        try {
            _loginService.RevokeRefreshToken(refreshToken);
            return Ok();
        } catch (Exception ex) {
            _logger.LogInformation(ex, "Revoke token failed");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
