namespace FashionStore.Infrastructure.Authentication;
public class JwtSettings
{
    public const string SectionName = "JwtSettings";
    public string SecretKey { get; init; }
    public int ExpiryMinutes { get; init; }
    public string ValidIssuer { get; init; }
    public string ValidAudience { get; init; }
}
