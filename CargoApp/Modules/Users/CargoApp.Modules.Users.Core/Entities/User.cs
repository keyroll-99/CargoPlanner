using CargoApp.Core.Infrastructure.Entites;

namespace CargoApp.Modules.Users.Core.Entities;

public class User : BaseEntity
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public bool IsActive { get; set; }

    public User(
        Guid id,
        string email,
        string password,
        bool isActive,
        DateTime createAt)
    {
        Id = id;
        Email = email;
        Password = password;
        IsActive = isActive;
        CreateAt = createAt;
    }

    public User()
    {
    }
}