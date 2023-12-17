using CargoApp.Core.ShareCore.Enums;
using CargoApp.Modules.Users.Core.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace CargoApp.Modules.Users.Core.DAL.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    private readonly IWebHostEnvironment _environment;
    private readonly IPasswordHasher<User> _passwordHasher;
    
    public UserConfiguration(IWebHostEnvironment environment, IPasswordHasher<User> passwordHasher)
    {
        _environment = environment;
        _passwordHasher = passwordHasher;
    }

    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Email).IsUnique();
        builder.Property(x => x.Password).IsRequired();

    }
}