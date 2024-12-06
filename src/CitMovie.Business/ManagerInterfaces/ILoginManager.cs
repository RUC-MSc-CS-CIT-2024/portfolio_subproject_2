namespace CitMovie.Business;

public interface ILoginManager {
    Task<TokenDto> AuthenticateAsync(string username, string password);
    Task<TokenDto> AuthenticateAsync(string refreshToken);
    void RevokeRefreshToken(string refreshToken);
}
