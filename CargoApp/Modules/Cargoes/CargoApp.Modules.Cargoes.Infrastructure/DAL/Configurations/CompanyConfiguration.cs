using CargoApp.Modules.Cargoes.Core.CompanyAggregate;
using CargoApp.Modules.Cargoes.Core.CompanyAggregate.ValueObject;
using CargoApp.Modules.Cargoes.Core.LocationAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CargoApp.Modules.Cargoes.Infrastructure.DAL.Configurations;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder
            .Property<CompanyName>(x => x.CompanyName)
            .HasConversion(x => x.Name, x => new CompanyName(x))
            .HasColumnName("CompanyName");

        builder.Property(x => x.CompanyId).HasColumnName("CompanyId");

        builder.OwnsMany<Driver>(d => d.Drivers, navigationBuilder =>
        {
            navigationBuilder.HasKey(x => x.Id);
            navigationBuilder.HasOne<Location>(x => x.Home).WithMany().HasForeignKey("HomeId");
            navigationBuilder.Property<bool>(x => x.IsActive).HasColumnName("IsActive");
            navigationBuilder.Property<Guid>(x => x.EmployeeId).HasColumnName("EmployeeId");
        });
    }
}