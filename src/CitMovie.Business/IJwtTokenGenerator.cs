namespace CitMovie.Business;

public interface IJwtTokenGenerator
{
    string GenerateEncodedToken(User user, IEnumerable<string> roles);
    string GenerateRefreshToken(string tokenId, int userId);
    (string tokenId, int userId) ParseRefreshTokenPayload(string token);
}
