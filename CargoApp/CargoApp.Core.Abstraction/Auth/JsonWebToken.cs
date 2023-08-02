namespace CargoApp.Core.Abstraction.Auth;

public record JsonWebToken
{
    public required string AccessToken { get; init; }
    public DateTime Expires { get; init; }
    public required string Email { get; init; }
    public required Guid UserId { get; init; }
}