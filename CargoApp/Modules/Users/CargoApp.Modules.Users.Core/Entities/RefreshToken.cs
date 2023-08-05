using CargoApp.Core.ShareCore.Clock;
using CargoApp.Core.ShareCore.Entites;

namespace CargoApp.Modules.Users.Core.Entities;

public class RefreshToken : BaseEntity
{
    public string Token { get; private set; }
    public DateTime ExpiredAt { get; private set; }
    public bool IsUsed { get; set; }
    public Guid UserId { get; private set; }
    public User User { get; private set; }

    public RefreshToken(Guid id, DateTime createAt, string token, DateTime expiredAt, bool isUsed, Guid userId,
        User user) : base(id, createAt)
    {
        Token = token;
        ExpiredAt = expiredAt;
        IsUsed = isUsed;
        UserId = userId;
        User = user;
    }

    public RefreshToken(Guid id, DateTime createAt, string token, DateTime expiredAt, bool isUsed, Guid userId) :
        base(id, createAt)
    {
        Token = token;
        ExpiredAt = expiredAt;
        IsUsed = isUsed;
        UserId = userId;
    }

    
    // maybe whole logic should be here?
    public static RefreshToken Create(Guid userId, string token, IClock clock)
    {
        return new RefreshToken(
            Guid.NewGuid(),
            clock.Now(),
            token,
            clock.Now().AddDays(7),
            false,
            userId);
    }
}