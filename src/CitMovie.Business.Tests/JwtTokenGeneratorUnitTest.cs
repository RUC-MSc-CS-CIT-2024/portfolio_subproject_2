using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using CitMovie.Models.DomainObjects;
using FakeItEasy;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CitMovie.Business.Tests;

public class JwtTokenGeneratorUnitTest 
{
    [Fact]
    public void GenerateEncodedToken_TokenIsGenerated_NonNullOrEmptyValueIsReturned() {
        // Arrange
        IOptionsSnapshot<JwtOptions> jwtSettings = CreateFakeJwtOptions();
        User user = new() {
            Username = A.Dummy<string>(),
            Email = A.Dummy<string>(),
            HashedPassword = A.Dummy<string>(),
            Salt = A.Dummy<string>()
        };

        JwtTokenGenerator generator = new(jwtSettings);

        // Act
        string token = generator.GenerateEncodedToken(user, []);

        // Assert
        Assert.NotNull(token);
        Assert.NotEmpty(token);
    }

    [Fact]
    public void GenerateEncodedToken_ValidJwtToken_NoErrorWhenParsingJwtToken() {
        // Arrange
        IOptionsSnapshot<JwtOptions> jwtSettings = CreateFakeJwtOptions();
        User user = new() {
            Username = A.Dummy<string>(),
            Email = A.Dummy<string>(),
            HashedPassword = A.Dummy<string>(),
            Salt = A.Dummy<string>()
        };

        JwtTokenGenerator generator = new(jwtSettings);

        // Act
        string token = generator.GenerateEncodedToken(user, []);

        // Assert
        new JwtSecurityTokenHandler().ReadJwtToken(token);
    }

    [Fact]
    public void GenerateEncodedToken_JwtOptionsAreUsedToGenerateToken_ValuesInJwtOptionsEqualValuesInToken() {
        // Arrange
        IOptionsSnapshot<JwtOptions> jwtSettings = CreateFakeJwtOptions();
        User user = new() {
            Username = A.Dummy<string>(),
            Email = A.Dummy<string>(),
            HashedPassword = A.Dummy<string>(),
            Salt = A.Dummy<string>()
        };

        JwtTokenGenerator generator = new(jwtSettings);

        // Act
        string tokenString = generator.GenerateEncodedToken(user, []);
        JwtSecurityToken token = new JwtSecurityTokenHandler().ReadJwtToken(tokenString);

        // Assert
        Assert.Equal(jwtSettings.Value.Issuer, token.Issuer);
        Assert.Equal(jwtSettings.Value.Audience, token.Audiences.First());
    }

    [Fact]
    public void GenerateEncodedToken_CorrectSubject_UsernameEqualsSubject() {
        // Arrange
        IOptionsSnapshot<JwtOptions> jwtSettings = CreateFakeJwtOptions();
        User user = new() {
            Username = A.Dummy<string>(),
            Email = A.Dummy<string>(),
            HashedPassword = A.Dummy<string>(),
            Salt = A.Dummy<string>()
        };

        JwtTokenGenerator generator = new(jwtSettings);

        // Act
        string tokenString = generator.GenerateEncodedToken(user, []);
        JwtSecurityToken token = new JwtSecurityTokenHandler().ReadJwtToken(tokenString);

        // Assert
        Assert.Equal(user.Username, token.Subject);
    }

    [Fact]
    public void GenerateEncodedToken_EmailIsPartOfPayload_TokenPayloadIncludesUserEmail() {
        // Arrange
        IOptionsSnapshot<JwtOptions> jwtSettings = CreateFakeJwtOptions();
        User user = new() {
            Username = A.Dummy<string>(),
            Email = A.Dummy<string>(),
            HashedPassword = A.Dummy<string>(),
            Salt = A.Dummy<string>()
        };

        JwtTokenGenerator generator = new(jwtSettings);

        // Act
        string tokenString = generator.GenerateEncodedToken(user, []);
        JwtSecurityToken token = new JwtSecurityTokenHandler().ReadJwtToken(tokenString);

        // Assert
        Assert.Equal(user.Email, token.Payload["email"]);
    }

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