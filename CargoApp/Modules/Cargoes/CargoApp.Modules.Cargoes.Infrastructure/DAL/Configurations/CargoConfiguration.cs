using CargoApp.Modules.Cargoes.Core.CargoAggregate;
using CargoApp.Modules.Cargoes.Core.CompanyAggregate;
using CargoApp.Modules.Cargoes.Core.DriverAggregate;
using CargoApp.Modules.Cargoes.Core.LocationAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CargoApp.Modules.Cargoes.Infrastructure.DAL.Configurations;

public class CargoConfiguration : IEntityTypeConfiguration<Cargo>
{
    public void Configure(EntityTypeBuilder<Cargo> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne<Location>(x => x.To).WithMany().HasForeignKey("LocationToId");
        builder.HasOne<Location>(x => x.From).WithMany().HasForeignKey("LocationFromId");
        builder.HasOne<Driver>(x => x.Driver).WithMany().HasForeignKey("DriverId");
        builder.HasOne<Company>(x => x.Sender).WithMany().HasForeignKey("SenderId");
        builder.HasOne<Company>(x => x.Receiver).WithMany().HasForeignKey("ReceiverId");
        builder.Property(x => x.ExpectedDeliveryTime).HasColumnName("ExpectedDeliveryTime");
        builder.Property(x => x.DeliveryDate).IsRequired(false).HasColumnName("DeliveryDate");
        builder.Property(x => x.IsLocked).IsRequired().HasColumnName("IsLocked");
        builder.Property(x => x.IsDelivered).HasColumnName("IsDelivered");
        builder.Property(x => x.IsCanceled).HasColumnName("IsCanceled");
    }
}