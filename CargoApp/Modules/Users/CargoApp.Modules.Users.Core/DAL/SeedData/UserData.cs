using CargoApp.Core.ShareCore.Enums;
using CargoApp.Modules.Users.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace CargoApp.Modules.Users.Core.DAL.SeedData;

internal class UserData
{
    private static readonly PasswordHasher<User> PasswordHasher = new();
    
    public static User User = new User
    {
        Email = "admin@admin.com",
        Password = PasswordHasher.HashPassword(null, "Test123!"),
        PermissionMask = (PermissionEnum)31,
        Id = Guid.Parse("2478b3ee-2924-49ba-bd43-27df230cea1e"),
        CreateAt = DateTime.UtcNow,
        EmployeeId = Guid.Parse("49ec2f72-be39-44ca-9f5d-d6816c305413"),
        IsActive = true,
    };
    
}