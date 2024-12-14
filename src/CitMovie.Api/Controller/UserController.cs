namespace CitMovie.Api;

[ApiController]
[Route("api/users")]
[Authorize(Policy = "user_scope")]
[Tags("User")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly PagingHelper _pagingHelper;
    private readonly IUserManager _userManager;
    private readonly ILoginManager _loginService;

    public UserController(ILogger<UserController> logger, PagingHelper pagingHelper, IUserManager userManager, ILoginManager loginService)
    {
        _logger = logger;
        _pagingHelper = pagingHelper;
        _userManager = userManager;
        _loginService = loginService;
    }

    [HttpGet("{userId}", Name = nameof(GetUser))]
    [HttpGet("/api/user")]
    public async Task<ActionResult> GetUser(int? userId)
    {   
        if (userId == null) {
            string? userIdString = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            if (int.TryParse(userIdString, out int parseResult))
                userId = parseResult;
        }
        
        if (!userId.HasValue)
            return Unauthorized("User not found");

        try
        {
            UserResult user = await _userManager.GetUserAsync(userId.Value);
            user.Links = Url.AddUserLinks(userId.Value);
            return Ok(user);
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex, "Get user failed");
            return NotFound("User not found");
        }
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<ActionResult> CreateUser([FromBody] UserCreateRequest user)
    {
        try
        {
            UserResult newUser = await _userManager.CreateUserAsync(user);
            TokenDto token = await _loginService.AuthenticateAsync(newUser.Username, user.Password);
            Response.Cookies.Append("access-token", token.AccessToken, new () {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Lax,
                Expires = DateTimeOffset.UtcNow.AddSeconds(token.ExpiresIn)
            });
            return Ok(token);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Create user failed");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPatch("{userId}")]
    public async Task<ActionResult> UpdateUser(int userId, [FromBody] UserUpdateRequest user)
    {
        try
        {
            UserResult updatedUser = await _userManager.UpdateUserAsync(userId, user);
            updatedUser.Links = Url.AddUserLinks(userId);
            return Ok(updatedUser);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Update user failed");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpDelete("{userId}")]
    public async Task<ActionResult> DeleteUser(int userId)
    {
        try
        {
            bool deleted = await _userManager.DeleteUserAsync(userId);
            return deleted
                ? NoContent()
                : NotFound("User not found");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Delete user failed");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}