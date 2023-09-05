using CargoApp.Modules.Companies.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CargoApp.Modules.Companies.Core.DAL.Configurations;

internal class WorkerConfiguration : IEntityTypeConfiguration<Worker>
{
    public void Configure(EntityTypeBuilder<Worker> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .HasOne(x => x.Company)
            .WithMany(x => x.Workers)
            .HasForeignKey(x => x.CompanyId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}