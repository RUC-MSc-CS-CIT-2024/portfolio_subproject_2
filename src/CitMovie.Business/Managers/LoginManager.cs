using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace CitMovie.Business;

public class LoginManager : ILoginManager
{
    private JwtOptions _config;

    public LoginManager(IOptionsSnapshot<JwtOptions> optionsAccessor)
    {
        _config = optionsAccessor.Value;
    }

    public string Login(string username, string password)
    {
        return GenerateEncodedToken(username, new List<string>());
    }

    public void Logout(string token)
    {
        throw new NotImplementedException();
    }

    public string RefreshToken(string token)
    {
        throw new NotImplementedException();
    }

    private string GenerateEncodedToken(string username, IList<string> roles)
    {
        List<Claim> claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, username),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.Ticks.ToString(), ClaimValueTypes.Integer64)
        };

        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_config.SigningKey));
        SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha256);
        JwtSecurityToken jwt = new(
            issuer: _config.Issuer,
            audience: _config.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddDays(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
}