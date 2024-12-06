public class RefreshToken
{
    public required string Value { get; set; }
    public required DateTimeOffset Expires { get; set; }
    public bool IsRevoked { get; set; } = false;
    public bool IsValid => DateTime.UtcNow < Expires && !IsRevoked;
}
