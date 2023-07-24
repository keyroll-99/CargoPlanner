namespace CargoApp.Modules.Users.Core.Commands;

public class SingInCommand
{
    public required string Email { get; init; }
    public required string Password { get; init; }
}