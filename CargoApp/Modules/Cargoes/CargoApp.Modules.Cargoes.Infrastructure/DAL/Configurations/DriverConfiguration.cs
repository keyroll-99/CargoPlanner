using CargoApp.Modules.Cargoes.Core.CargoAggregate;
using CargoApp.Modules.Cargoes.Core.CompanyAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CargoApp.Modules.Cargoes.Infrastructure.DAL.Configurations;

public class DriverConfiguration : IEntityTypeConfiguration<Driver>
{
    public void Configure(EntityTypeBuilder<Driver> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne<Location>(x => x.Home).WithMany().HasForeignKey("HomeId");
        builder.Property<bool>(x => x.IsActive).HasColumnName("IsActive");
        builder.Property<Guid>(x => x.EmployeeId).HasColumnName("EmployeeId");
    }
}