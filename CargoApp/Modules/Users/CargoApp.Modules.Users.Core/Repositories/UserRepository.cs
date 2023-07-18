using CargoApp.Core.Infrastructure.Repositories;
using CargoApp.Modules.Users.Core.DAL;
using CargoApp.Modules.Users.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CargoApp.Modules.Users.Core.Repositories;

public class UserRepository : Repository<User, UserDbContext>,  IUserRepository
{
    public UserRepository(UserDbContext appContext) : base(appContext)
    {
    }

    public Task<bool> ExistsByEmail(string email)
    {
        return Entities.AnyAsync(x => x.Email == email);
    }
}