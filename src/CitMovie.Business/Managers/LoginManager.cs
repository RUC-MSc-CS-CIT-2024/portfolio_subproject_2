using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace CitMovie.Business;

public class LoginManager : ILoginManager
{
    private readonly IUserRepository _repository;
    private JwtOptions _config;

    public LoginManager(IUserRepository repository, IOptionsSnapshot<JwtOptions> optionsAccessor)
    {
        _config = optionsAccessor.Value;
        _repository = repository;
    }

    public async Task<string> LoginAsync(string username, string password)
    {
        User user = await _repository.GetUserAsync(username);
        if (user.Password != password)
            throw new UnauthorizedAccessException();

        return GenerateEncodedAccessToken(user, new List<string> {"default"}); 
    }

    public Task<string> RefreshTokenAsync(string token)
    {
        throw new NotImplementedException();
    }

    public Task Revoke(string token)
    {
        throw new NotImplementedException();
    }

    private string GenerateEncodedAccessToken(User user, IEnumerable<string> roles)
    {
        DateTime now = DateTime.UtcNow;
        JwtSecurityTokenHandler handler = new();

        List<Claim> claims = [
            new(JwtRegisteredClaimNames.Sub, user.Username),
            new(JwtRegisteredClaimNames.Email, user.Email),
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