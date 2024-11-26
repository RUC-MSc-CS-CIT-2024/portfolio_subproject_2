namespace CitMovie.Business;

public interface IPasswordHelper {
    string GenerateSalt();
    string HashPassword(string password, string salt);
    bool VerifyPassword(string password, string salt, string hashedPassword);
} 