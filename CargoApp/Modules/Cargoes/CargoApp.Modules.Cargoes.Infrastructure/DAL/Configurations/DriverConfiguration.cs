using CargoApp.Modules.Cargoes.Core.CompanyAggregate;
using CargoApp.Modules.Cargoes.Core.DriverAggregate;
using CargoApp.Modules.Cargoes.Core.LocationAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CargoApp.Modules.Cargoes.Infrastructure.DAL.Configurations;

public class DriverConfiguration : IEntityTypeConfiguration<Driver>
{
    public void Configure(EntityTypeBuilder<Driver> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne<Location>(x => x.Home).WithMany().HasForeignKey("HomeId");
        builder.HasOne<Company>(x => x.Employer).WithMany(x => x.Drivers).HasForeignKey("EmployerId");

        builder.Property<bool>(x => x.IsActive).HasColumnName("IsActive");
        builder.Property<Guid>(x => x.EmployeeId).HasColumnName("EmployeeId");
    }
}