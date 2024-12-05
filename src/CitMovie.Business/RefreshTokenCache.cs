using Microsoft.Extensions.DependencyInjection;

namespace CitMovie.Business;

public class RefreshTokenCache : IRefreshTokenCache
{
    private readonly Dictionary<string, RefreshToken> _items;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly JwtOptions _jwtOptions;

    public RefreshTokenCache(
        [FromKeyedServices("refreshTokenCache")] Dictionary<string, RefreshToken> refreshTokenList,
        IJwtTokenGenerator jwtTokenGenerator,
        IOptionsSnapshot<JwtOptions> jwtOptionsAccessor)
    {
        _jwtOptions = jwtOptionsAccessor.Value;
        _items = refreshTokenList;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public string Generate(int userId)
    {
        string refreshToken = Guid.NewGuid().ToString();
        RefreshToken token = new() {
            Value = _jwtTokenGenerator.GenerateRefreshToken(refreshToken, userId),
            Expires = DateTimeOffset.UtcNow.AddSeconds(_jwtOptions.RefreshTokenLifetimeSeconds)
        };
        _items.Add(refreshToken, token);
        return token.Value;
    }

    public bool IsValid(string refreshToken)
    {
        if (!_items.ContainsKey(refreshToken))
            return false;

        RefreshToken token = _items[refreshToken];
        if (token.IsValid)
            return true;
        
        token.IsRevoked = true;
        return false;
    }

    public void Revoke(string refreshToken)
    {
        if (_items.TryGetValue(refreshToken, out RefreshToken? token))
            token.IsRevoked = true;
    }
}
