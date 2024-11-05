namespace CitMovie.Models.DataTransferObjects;

public class UserResult
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public List<Link> Links { get; set; } = new List<Link>();
}
public record UserCreateRequest(string Username, string Email, string Password);
public record UserUpdateRequest(string? Username, string? Email, string? Password);
