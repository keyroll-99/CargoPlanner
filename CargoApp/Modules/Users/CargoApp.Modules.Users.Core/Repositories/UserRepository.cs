using CargoApp.Core.Infrastructure.Repositories;
using CargoApp.Core.ShareCore.Clock;
using CargoApp.Modules.Contracts.Events.Companies;
using CargoApp.Modules.Users.Core.DAL;
using CargoApp.Modules.Users.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Result;

namespace CargoApp.Modules.Users.Core.Repositories;

internal class UserRepository : Repository<User, UserDbContext>, IUserRepository
{
    public UserRepository(UserDbContext appContext, IClock clock) : base(appContext, clock)
    {
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await Entities.AnyAsync(x => x.Email == email);
    }

    public Task<User?> GetByEmailAsync(string email)
    {
        return Entities.SingleOrDefaultAsync(x => x.Email == email);
    }

    public Task<User?> GetByEmployeeId(Guid employeeId)
    {
        return Entities.FirstOrDefaultAsync(x => x.EmployeeId == employeeId);
    }

    public async Task<Result<User>> AddAsync(EmployeeCreateEvent @event)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = @event.Email,
            Password = "#TemporaryPassword#",
            CreateAt = Clock.Now(),
            EmployeeId = @event.Id,
            IsActive = true,
            PermissionMask = 0,
            RefreshTokens = new List<RefreshToken>()
        };

        await AddAsync(user);
        return user;
    }
}