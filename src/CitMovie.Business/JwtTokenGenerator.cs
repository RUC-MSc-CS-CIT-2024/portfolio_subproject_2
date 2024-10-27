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
            expires: DateTime.UtcNow.AddMinutes(15),
            issuedAt: now,
            signingCredentials: creds
        );

        return token;
    }
}