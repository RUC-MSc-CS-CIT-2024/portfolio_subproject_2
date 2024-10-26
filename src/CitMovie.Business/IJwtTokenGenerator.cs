namespace CitMovie.Business;

public interface IJwtTokenGenerator
{
    string GenerateEncodedToken(User user, IEnumerable<string> roles);
}
