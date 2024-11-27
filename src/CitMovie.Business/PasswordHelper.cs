using System.Security.Cryptography;

namespace CitMovie.Business;

public class PasswordHelper : IPasswordHelper
{
    const int salt_bitsize = 128;
    const int hash_bitsize = 256;
    const int iterations = 500000;

    public string GenerateSalt()
    {
        using RandomNumberGenerator rng = RandomNumberGenerator.Create();
        byte[] salt = new byte[salt_bitsize / 8];
        rng.GetBytes(salt);
        return Convert.ToBase64String(salt);
    }

    public string HashPassword(string password, string salt)
    {
        byte[] saltBytes = Convert.FromBase64String(salt);
        using Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, iterations, HashAlgorithmName.SHA256);
        byte[] hash = pbkdf2.GetBytes(hash_bitsize / 8);
        return Convert.ToBase64String(hash);
    }

    public bool VerifyPassword(string password, string salt, string hashedPassword)
    {
        string computedHash = HashPassword(password, salt);
        return computedHash == hashedPassword;
    }
}