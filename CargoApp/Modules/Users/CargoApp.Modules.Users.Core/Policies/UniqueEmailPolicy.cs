using CargoApp.Core.Abstraction.Policies;
using CargoApp.Modules.Users.Core.Commands;
using CargoApp.Modules.Users.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CargoApp.Modules.Users.Core.Policies;

public class UniqueEmailPolicy : IPolicy<CreateUserCommand>
{

    private DbSet<User> Users;

    public bool CanBeApplied(CreateUserCommand model)
        => true;

    public bool IsValid(CreateUserCommand model)
    {
        throw new NotImplementedException();
    }
}