using System.Security.Cryptography;
using CitMovie.Data;
using CitMovie.Models.DomainObjects;
using FakeItEasy;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CitMovie.Business.Tests;

public class LoginManagerUnitTest
{
    [Fact]
    public async Task AuthenticateAsync_PasswordDoesNotMatch_UnauthorizedAccessExceptionIsThrown()
    {
        // Arrange
        IUserRepository repository = A.Fake<IUserRepository>();
        A.CallTo(() => repository.GetUserAsync("username")).Returns(new User { Username = "username", HashedPassword = "password", Email = "email", Salt = "salt" });

        IPasswordHelper passwordHelper = A.Fake<IPasswordHelper>();
        A.CallTo(() => passwordHelper.VerifyPassword(A.Dummy<string>(), A.Dummy<string>(), A.Dummy<string>())).Returns(false);

        RefreshTokenCache refreshTokenCache = A.Fake<RefreshTokenCache>();
        A.CallTo(() => refreshTokenCache.Generate(A<int>.Ignored)).Returns("refreshToken");

        LoginManager userRepository = new LoginManager(repository, A.Fake<IJwtTokenGenerator>(), passwordHelper, refreshTokenCache, CreateFakeJwtOptions());
        

        // Act
        Func<Task> testCode = () => userRepository.AuthenticateAsync("username", "invalidPassword");

        // Act & Assert
        await Assert.ThrowsAsync<UnauthorizedAccessException>(testCode);
    }

    [Fact]
    public async Task AuthenticateAsync_PasswordMatch_JwtTokenIsReturned()
    {
        // Arrange
        IUserRepository repository = A.Fake<IUserRepository>();
        A.CallTo(() => repository.GetUserAsync(A<string>.Ignored)).Returns(new User { Username = "username", HashedPassword = "password", Email = "email", Salt = "salt" });

        IPasswordHelper passwordHelper = A.Fake<IPasswordHelper>();
        A.CallTo(() => passwordHelper.VerifyPassword(A<string>.Ignored, A<string>.Ignored, A<string>.Ignored)).Returns(true);

        IJwtTokenGenerator jwtTokenGenerator = A.Fake<IJwtTokenGenerator>();
        A.CallTo(() => jwtTokenGenerator.GenerateEncodedToken(A<User>.Ignored, A<IEnumerable<string>>.Ignored)).Returns("token");

        RefreshTokenCache refreshTokenCache = A.Fake<RefreshTokenCache>();
        A.CallTo(() => refreshTokenCache.Generate(A<int>.Ignored)).Returns("refreshToken");

        LoginManager userRepository = new LoginManager(repository, A.Fake<IJwtTokenGenerator>(), passwordHelper, refreshTokenCache, CreateFakeJwtOptions());

        // Act
        TokenDto token = await userRepository.AuthenticateAsync("username", "password");

        // Assert
        Assert.Equal("token", token.AccessToken);
    }

    // AuthenticateAsync_InvalidRefreshToken_
    // AuthenticateAsync_ExpiredRefreshToken_
    // AuthenticateAsync_ValidRefreshToken_

    // RevokeRefreshToken_InvalidRefreshToken_
    // RevokeRefreshToken_ExpiredRefreshToken_
    // RevokeRefreshToken_ValidRefreshToken_TokenIsRevokedAndCannotBeUsedAgain

    private static IOptionsSnapshot<JwtOptions> CreateFakeJwtOptions() {
        JwtOptions jwtConf = new() {
            Audience = "audience",
            Issuer = "issuer",
            SigningKey = new RsaSecurityKey(new RSACryptoServiceProvider(512)).ToString()
        };
        IOptionsSnapshot<JwtOptions> jwtSettings = A.Fake<IOptionsSnapshot<JwtOptions>>();
        A.CallTo(() => jwtSettings.Value).Returns(jwtConf);

        return jwtSettings;
    }
}
