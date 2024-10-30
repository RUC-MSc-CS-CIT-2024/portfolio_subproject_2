namespace CitMovie.Business;

public interface ILoginManager {
    Task<string> LoginAsync(string username, string password);
    Task Revoke(string token);
    Task<string> RefreshTokenAsync(string token);
}