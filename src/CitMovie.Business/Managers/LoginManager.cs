namespace CitMovie.Business;

public class LoginManager : ILoginManager
{
    private readonly IUserRepository _repository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginManager(IUserRepository repository, IJwtTokenGenerator jwtTokenGenerator)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _repository = repository;
    }

    public async Task<string> LoginAsync(string username, string password)
    {
        User user = await _repository.GetUserAsync(username);
        if (user.Password != password)
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