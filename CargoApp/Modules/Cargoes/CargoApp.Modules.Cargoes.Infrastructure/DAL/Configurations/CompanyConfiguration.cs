using CargoApp.Modules.Cargoes.Core.CompanyAggregate;
using CargoApp.Modules.Cargoes.Core.CompanyAggregate.ValueObject;
using CargoApp.Modules.Cargoes.Core.DriverAggregate;
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

        builder.HasMany<Driver>(x => x.Drivers).WithOne(y => y.Employer);
    }
}