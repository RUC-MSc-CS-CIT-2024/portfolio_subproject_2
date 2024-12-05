namespace CitMovie.Business;

public class LoginManager : ILoginManager
{
    private readonly IUserRepository _repository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IPasswordHelper _passwordHelper;
    private readonly IRefreshTokenCache _refreshTokenCache;
    private readonly JwtOptions _jwtOptions;

    public LoginManager(
        IUserRepository repository, 
        IJwtTokenGenerator jwtTokenGenerator, 
        IPasswordHelper passwordHelper, 
        IRefreshTokenCache refreshTokenCache,
        IOptionsSnapshot<JwtOptions> jwtOptionsAccessor)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _passwordHelper = passwordHelper;
        _refreshTokenCache = refreshTokenCache;

        _jwtOptions = jwtOptionsAccessor.Value;

        _repository = repository;
    }

    public async Task<TokenDto> AuthenticateAsync(string username, string password)
    {
        User user = await _repository.GetUserAsync(username);
        if (!_passwordHelper.VerifyPassword(password, user.Salt, user.HashedPassword))
            throw new UnauthorizedAccessException();

        TokenDto token = new TokenDto
        {
            AccessToken = _jwtTokenGenerator.GenerateEncodedToken(user, new List<string> {"default"}),
            RefreshToken = _refreshTokenCache.Generate(user.Id),
            ExpiresIn = _jwtOptions.TokenLifetimeSeconds
        };
        return token;
    }   

    public async Task<TokenDto> AuthenticateAsync(string refreshToken)
    {
        (string tokenId, int userId) = _jwtTokenGenerator.ParseRefreshTokenPayload(refreshToken);

        if (!_refreshTokenCache.IsValid(tokenId))
            throw new UnauthorizedAccessException();

        User user = await _repository.GetUserAsync(userId);
        return new TokenDto
        {
            AccessToken = _jwtTokenGenerator.GenerateEncodedToken(user, new List<string> {"default"}),
            RefreshToken = _refreshTokenCache.Generate(userId),
            ExpiresIn = _jwtOptions.TokenLifetimeSeconds
        };
    }

    public void RevokeRefreshToken(string refreshToken)
    {
        (string tokenId, _) = _jwtTokenGenerator.ParseRefreshTokenPayload(refreshToken);
        _refreshTokenCache.Revoke(tokenId);
    }
}
