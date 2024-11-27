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
    [MinLength(15), MaxLength(64)]
    public required string Password { get; set; }
}

public record UserUpdateRequest {
    public string? Username { get; set; }
    public string? Email { get; set; }
    [MinLength(15), MaxLength(64)]
    public string? Password { get; set; }
};
