namespace CitMovie.Business;

public interface ILoginManager {
    string Login(string username, string password);
    void Logout(string token);

    string RefreshToken(string token);
}