using FashionStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionStore.Infrastructure.Persistence.Configurations;

public class SkuImageConfiguration : IEntityTypeConfiguration<SkuImage>
{
    public void Configure(EntityTypeBuilder<SkuImage> builder)
    {
        builder.HasKey(si => si.SkuImageId);

        builder.Property(si => si.Image)
            .IsRequired()
            .HasMaxLength(500);

        builder.HasOne(si => si.Sku)
            .WithMany(s => s.SkuImages)
            .HasForeignKey(si => si.SkuId);
    }
}
