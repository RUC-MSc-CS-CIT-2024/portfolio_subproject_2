namespace CitMovie.Models.DataTransferObjects;

public record UserResult(int Id, string Username, string Email);
public record UserCreateRequest(string Username, string Email, string Password);
public record UserUpdateRequest(string? Username, string? Email, string? Password);
