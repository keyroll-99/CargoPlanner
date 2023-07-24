using CargoApp.Modules.Users.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace CargoApp.Modules.Users.Core.Security;

public sealed class PasswordManager
{
    private readonly IPasswordHasher<User> _passwordHasher;

    public PasswordManager(IPasswordHasher<User> passwordHasher)
    {
        _passwordHasher = passwordHasher;
    }

    public string Hash(string password)
    {
        return _passwordHasher.HashPassword(default!, password);
    }

    public bool Validate(string password, string securedPassword)
    {
        return _passwordHasher.VerifyHashedPassword(default!, securedPassword, password) ==
               PasswordVerificationResult.Success;
    }
}