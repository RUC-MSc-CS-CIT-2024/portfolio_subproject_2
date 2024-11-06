namespace CitMovie.Models.DataTransferObjects;

public class UserResult : BaseResult
{
    public required int Id { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
}

public record UserCreateRequest {
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}

public record UserUpdateRequest(string? Username, string? Email, string? Password);
