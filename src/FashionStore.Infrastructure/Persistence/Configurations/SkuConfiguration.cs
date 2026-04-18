using FashionStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionStore.Infrastructure.Persistence.Configurations;

public class SkuConfiguration : IEntityTypeConfiguration<Sku>
{
    public void Configure(EntityTypeBuilder<Sku> builder)
    {
        builder.HasKey(s => s.SkuId);

        builder.Property(s => s.SkuCode)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.Barcode)
            .HasMaxLength(100);

        builder.HasOne(s => s.Product)
            .WithMany(p => p.Skus)
            .HasForeignKey(s => s.ProductId);
    }
}
