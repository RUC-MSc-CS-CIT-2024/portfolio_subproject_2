namespace CitMovie.Business;

public interface IRefreshTokenCache
{
    string Generate(int userId);
    bool IsValid(string refreshToken);
    void Revoke(string refreshToken);
}
