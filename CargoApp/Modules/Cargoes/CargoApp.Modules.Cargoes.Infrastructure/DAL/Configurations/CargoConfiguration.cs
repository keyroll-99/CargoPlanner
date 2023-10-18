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
        builder.HasOne<Location>("_to").WithMany().HasForeignKey("LocationToId");
        builder.HasOne<Location>("_from").WithMany().HasForeignKey("LocationFromId");
        builder.HasOne<Driver>("_driver").WithMany();
        builder.HasOne<Company>("_sender").WithMany();
        builder.HasOne<Company>("_receiver").WithMany();
        builder.Property<DateTime>("_expectedDeliveryTime");
        builder.Property<DateTime?>("_deliveryDate").IsRequired(false);
    }
}