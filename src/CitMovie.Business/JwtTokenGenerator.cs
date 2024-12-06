using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace CitMovie.Business;

public class JwtTokenGenerator : IJwtTokenGenerator {
    private readonly JwtOptions _config;
    public JwtTokenGenerator(IOptionsSnapshot<JwtOptions> optionsAccessor)
    {
        _config = optionsAccessor.Value;
    }

    public string GenerateEncodedToken(User user, IEnumerable<string> roles)
    {
        DateTime now = DateTime.UtcNow;
        JwtSecurityTokenHandler handler = new();

        List<Claim> claims = [
            new(JwtRegisteredClaimNames.Sub, user.Username),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new("user_id", user.Id.ToString()),
            .. roles.Select(role => new Claim(ClaimTypes.Role, role)),
        ];

        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_config.SigningKey));
        SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha256);
        string token = handler.CreateEncodedJwt(
            _config.Issuer,
            _config.Audience,
            new(claims),
            notBefore: now,
            expires: DateTime.UtcNow.AddSeconds(_config.TokenLifetimeSeconds),
            issuedAt: now,
            signingCredentials: creds
        );

        return token;
    }

    public string GenerateRefreshToken(string tokenId, int userId)
    {
        DateTime now = DateTime.UtcNow;
        JwtSecurityTokenHandler handler = new();

        List<Claim> claims = [
            new(JwtRegisteredClaimNames.Sub, tokenId),
            new("user_id", userId.ToString()),
        ];

        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_config.SigningKey));
        SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha256);

        return handler.CreateEncodedJwt(
            _config.Issuer,
            _config.Audience,
            new(claims),
            notBefore: now,
            expires: DateTime.UtcNow.AddSeconds(_config.RefreshTokenLifetimeSeconds),
            issuedAt: now,
            signingCredentials: creds
        );
    }

    public (string tokenId, int userId) ParseRefreshTokenPayload(string token)
    {
        JwtSecurityTokenHandler handler = new();
        JwtSecurityToken jwtToken = handler.ReadJwtToken(token);

        string tokenId = jwtToken.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Sub).Value;
        string userId = jwtToken.Claims.First(claim => claim.Type == "user_id").Value;

        return (tokenId, int.Parse(userId));
    }
}
