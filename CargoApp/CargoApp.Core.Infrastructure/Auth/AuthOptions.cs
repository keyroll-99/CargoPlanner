namespace CargoApp.Core.Infrastructure.Auth;

public class AuthOptions
{
    public string Issuer { get; init; }
    public string IssuerSigningKey { get; init; }
    public string ValidIssuer { get; init; }
    public bool ValidateAudience { get; init; }
    public bool ValidateLifetime { get; init; }
    public bool ValidateIssuer { get; init; }
    public TimeSpan Expiry { get; init; }
}