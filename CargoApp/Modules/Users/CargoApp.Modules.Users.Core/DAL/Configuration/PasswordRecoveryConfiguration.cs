using CargoApp.Modules.Users.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CargoApp.Modules.Users.Core.DAL.Configuration;

public class PasswordRecoveryConfiguration : IEntityTypeConfiguration<PasswordRecovery>
{
    public void Configure(EntityTypeBuilder<PasswordRecovery> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .HasOne<User>(x => x.User)
            .WithMany(x => x.PasswordRecoveries)
            .HasForeignKey(x => x.UserId);
    }
}