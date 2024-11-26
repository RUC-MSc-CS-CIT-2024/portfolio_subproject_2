namespace CitMovie.Business;

public class LoginManager : ILoginManager
{
    private readonly IUserRepository _repository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IPasswordHelper _passwordHelper;

    public LoginManager(IUserRepository repository, IJwtTokenGenerator jwtTokenGenerator, IPasswordHelper passwordHelper)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _passwordHelper = passwordHelper;
        _repository = repository;
    }

    public async Task<string> LoginAsync(string username, string password)
    {
        User user = await _repository.GetUserAsync(username);
        if (!_passwordHelper.VerifyPassword(password, user.Salt, user.HashedPassword))
            throw new UnauthorizedAccessException();

        return _jwtTokenGenerator.GenerateEncodedToken(user, new List<string> {"default"}); 
    }

    public Task<string> RefreshTokenAsync(string token)
    {
        throw new NotImplementedException();
    }

    public Task Revoke(string token)
    {
        throw new NotImplementedException();
    }
}