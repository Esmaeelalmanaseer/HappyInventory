using HappyInventory.API.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HappyInventory.API.Data.Configurations;

public class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.HasKey(i => i.Id);

        builder.Property(x => x.Id);


          builder.Property(i => i.Id)
            .ValueGeneratedOnAdd();

        builder.Property(i => i.SKUCode)
            .HasMaxLength(50);

        builder.Property(i => i.Qty)
            .HasDefaultValue(1)
            .IsRequired();

        builder.Property(i => i.CostPrice)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(i => i.MSRPPrice)
            .HasColumnType("decimal(18,2)");

        builder.HasIndex(i => i.Name)
            .IsUnique();

        builder.HasIndex(i => i.SKUCode)
            .IsUnique()
            .HasFilter("[SKUCode] IS NOT NULL");

        builder.HasOne(i => i.Warehouse)
            .WithMany(w => w.Items)
            .HasForeignKey(i => i.WarehouseId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable("Items");
    }
}
