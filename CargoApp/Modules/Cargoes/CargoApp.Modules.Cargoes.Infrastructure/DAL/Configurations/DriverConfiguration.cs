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
        builder.HasOne<Location>("_home").WithMany().HasForeignKey("HomeId");
        builder.HasOne<Company>("_employer").WithMany("_drivers").HasForeignKey("EmployerId");

        builder.Property<bool>("_isActive").HasColumnName("IsActive");
    }
}