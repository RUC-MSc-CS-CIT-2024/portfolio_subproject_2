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
        A.CallTo(() => repository.GetUserAsync("username")).Returns(new User { Username = "username", Password = "password", Email = "email" });

        LoginManager userRepository = new LoginManager(repository, A.Fake<IJwtTokenGenerator>());

        // Act
        Func<Task> testCode = () => userRepository.LoginAsync("username", "invalidPassword");

        // Act & Assert
        await Assert.ThrowsAsync<UnauthorizedAccessException>(testCode);
    }
}