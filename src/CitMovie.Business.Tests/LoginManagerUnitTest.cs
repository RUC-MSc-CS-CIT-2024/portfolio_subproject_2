using CitMovie.Data;
using CitMovie.Models.DomainObjects;
using FakeItEasy;
using Microsoft.Extensions.Options;

namespace CitMovie.Business.Tests;

public class LoginManagerUnitTest
{
    [Fact]
    public async Task LoginAsync_PasswordDoesNotMatch_UnauthorizedAccessExceptionIsThrown()
    {
        // Arrange
        IUserRepository repository = A.Fake<IUserRepository>();
        A.CallTo(() => repository.GetUserAsync("username")).Returns(new User { Username = "username", HashedPassword = "password", Email = "email", Salt = "salt" });

        IPasswordHelper passwordHelper = A.Fake<IPasswordHelper>();
        A.CallTo(() => passwordHelper.VerifyPassword(A.Dummy<string>(), A.Dummy<string>(), A.Dummy<string>())).Returns(false);

        LoginManager userRepository = new LoginManager(repository, A.Fake<IJwtTokenGenerator>(), passwordHelper);
        

        // Act
        Func<Task> testCode = () => userRepository.LoginAsync("username", "invalidPassword");

        // Act & Assert
        await Assert.ThrowsAsync<UnauthorizedAccessException>(testCode);
    }

    [Fact]
    public async Task LoginAsync_PasswordMatch_JwtTokenIsReturned()
    {
        // Arrange
        IUserRepository repository = A.Fake<IUserRepository>();
        A.CallTo(() => repository.GetUserAsync(A<string>.Ignored)).Returns(new User { Username = "username", HashedPassword = "password", Email = "email", Salt = "salt" });

        IPasswordHelper passwordHelper = A.Fake<IPasswordHelper>();
        A.CallTo(() => passwordHelper.VerifyPassword(A<string>.Ignored, A<string>.Ignored, A<string>.Ignored)).Returns(true);

        IJwtTokenGenerator jwtTokenGenerator = A.Fake<IJwtTokenGenerator>();
        A.CallTo(() => jwtTokenGenerator.GenerateEncodedToken(A<User>.Ignored, A<IEnumerable<string>>.Ignored)).Returns("token");

        LoginManager userRepository = new LoginManager(repository, jwtTokenGenerator, passwordHelper);

        // Act
        string token = await userRepository.LoginAsync("username", "password");

        // Assert
        Assert.Equal("token", token);
    }
}