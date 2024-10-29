namespace CitMovie.Api;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase {
    private readonly ILogger<UserController> _logger;
    private readonly IUserManager _userManager;

    public UserController(ILogger<UserController> logger, IUserManager userManager)
    {
        _logger = logger;
        _userManager = userManager;
    }

    [Authorize(Policy = "user_scope")]
    [HttpGet("{userId}")]
    public async Task<ActionResult> GetUser(int userId) {
        try {
            UserResponse user = await _userManager.GetUserAsync(userId);
            return Ok(user);
        } catch (Exception ex) {
            _logger.LogInformation(ex, "Get user failed");
            return NotFound("User not found");
        }
    }

    [HttpPost]
    public async Task<ActionResult> CreateUser([FromBody] UserCreateRequest user) {
        try {
            UserResponse newUser = await _userManager.CreateUserAsync(user);
            return CreatedAtAction(nameof(GetUser), new { id = newUser.Id }, newUser);
        } catch (Exception ex) {
            _logger.LogInformation(ex, "Create user failed");
            return BadRequest("Create user failed");
        }
    }

    [Authorize(Policy = "user_scope")]
    [HttpPatch("{userId}")]
    public async Task<ActionResult> UpdateUser(int userId, [FromBody] UserUpdateRequest user) {
        try {
            UserResponse updatedUser = await _userManager.UpdateUserAsync(userId, user);
            return Ok(updatedUser);
        } catch (Exception ex) {
            _logger.LogInformation(ex, "Update user failed");
            return BadRequest("Update user failed");
        }
    }

    [Authorize(Policy = "user_scope")]
    [HttpDelete("{userId}")]
    public async Task<ActionResult> DeleteUser(int userId) {
        try {
            bool deleted = await _userManager.DeleteUserAsync(userId);
            return deleted 
                ? NoContent() 
                : NotFound("User not found");
        } catch (Exception ex) {
            _logger.LogInformation(ex, "Delete user failed");
            return BadRequest("Delete user failed");
        }
    }
}