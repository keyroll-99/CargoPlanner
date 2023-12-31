using CargoApp.Core.ShareCore.Clock;
using CargoApp.Core.ShareCore.Entites;

namespace CargoApp.Modules.Users.Core.Entities;

public class PasswordRecovery : BaseEntity
{
    public required Guid UserId { get; init; }
    public User User { get; init; }
    public DateTime ExpiredAt { get; private set; }
    public bool IsUsed { get; set; } = true;

    public void Revoke()
    {
        IsUsed = false;
    }

    public static PasswordRecovery CreatePasswordRecovery(User user, IClock clock)
    {
        return new PasswordRecovery
        {
            User = user,
            UserId = user.Id,
            ExpiredAt = clock.Now().AddHours(3),
            IsUsed = false,
            Id = Guid.NewGuid(),
            CreateAt = clock.Now()
        };
    }   
    
    public static PasswordRecovery CreatePasswordRecovery(Guid userId, IClock clock)
    {
        return new PasswordRecovery
        {
            UserId = userId,
            ExpiredAt = clock.Now().AddHours(3),
            IsUsed = false,
            Id = Guid.NewGuid(),
            CreateAt = clock.Now()
        };
    }

    public bool IsValid(IClock clock)
    {
        var now = clock.Now();
        return !IsUsed && ExpiredAt >= now;
    }
}