using CargoApp.Core.Infrastructure.Repositories;
using CargoApp.Modules.Users.Core.DAL;
using CargoApp.Modules.Users.Core.Entities;

namespace CargoApp.Modules.Users.Core.Repositories;

public class UserRepository : Repository<User, UserDbContext>,  IUserRepository
{
    public UserRepository(UserDbContext appContext) : base(appContext)
    {
    }
}