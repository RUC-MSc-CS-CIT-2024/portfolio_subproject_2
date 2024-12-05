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
    public async Task<ActionResult> GetUser(int userId)
    {
        try
        {
            UserResult user = await _userManager.GetUserAsync(userId);
            user.Links = AddUserkLinks(user);
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

    [Authorize(Policy = "user_scope")]
    [HttpPatch("{userId}")]
    public async Task<ActionResult> UpdateUser(int userId, [FromBody] UserUpdateRequest user)
    {
        try
        {
            UserResult updatedUser = await _userManager.UpdateUserAsync(userId, user);
            return Ok(updatedUser);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Update user failed");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [Authorize(Policy = "user_scope")]
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

    private List<Link> AddUserkLinks(UserResult user)
        => [ new Link {
                Href = _pagingHelper.GetResourceLink(nameof(GetUser), new { userId = user.Id }) ?? string.Empty,
                Rel = "self",
                Method = "GET"
            }
        ];
}