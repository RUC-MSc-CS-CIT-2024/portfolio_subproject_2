namespace CitMovie.Models.DataTransferObjects;

public class UserResult : BaseResult
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
}

public record UserCreateRequest(string Username, string Email, string Password);
public record UserUpdateRequest(string? Username, string? Email, string? Password);
