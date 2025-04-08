using HappyInventory.API.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HappyInventory.API.Data.Configurations;

public class WarehouseConfiguration : IEntityTypeConfiguration<Warehouse>
{
    public void Configure(EntityTypeBuilder<Warehouse> builder)
    {
        builder.HasKey(w => w.Id);


        builder.Property(i => i.Id)
          .ValueGeneratedOnAdd();

        builder.Property(w => w.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(w => w.Address)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(w => w.City)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(w => w.Country)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(w => w.Name)
            .IsUnique();

        builder.HasMany(w => w.Items)
            .WithOne(i => i.Warehouse)
            .HasForeignKey(i => i.WarehouseId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable("Warehouses");
    }
}