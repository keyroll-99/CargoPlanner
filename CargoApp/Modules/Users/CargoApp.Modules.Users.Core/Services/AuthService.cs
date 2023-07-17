using CargoApp.Modules.Users.Core.Commands;
using CargoApp.Modules.Users.Core.DTO;
using CargoApp.Modules.Users.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CargoApp.Modules.Users.Core.Services;

public class AuthService : IAuthService
{
    public DbSet<User> Users;

    public AuthService(DbSet<User> users)
    {
        Users = users;
    }

    public async Task<UserDto> CreateUser(CreateUserCommand createUserCommand)
    {
        return null;
    }
}