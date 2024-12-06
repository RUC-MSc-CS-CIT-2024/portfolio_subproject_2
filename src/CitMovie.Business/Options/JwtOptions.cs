namespace CitMovie.Business;

public class JwtOptions
{
    public string Issuer { get; set; } = "CitMovie_Api";
    public required string Audience { get; set; } = "CitMovie_Api";
    public required string SigningKey { get; set; }
    public int TokenLifetimeSeconds { get; set; } = 3600;
    public int RefreshTokenLifetimeSeconds { get; set; } = 43200;
}
