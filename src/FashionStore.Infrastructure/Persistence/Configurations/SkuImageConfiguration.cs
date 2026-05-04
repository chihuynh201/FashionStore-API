using FashionStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionStore.Infrastructure.Persistence.Configurations;

public class SkuImageConfiguration : IEntityTypeConfiguration<SkuImage>
{
    public void Configure(EntityTypeBuilder<SkuImage> builder)
    {
        builder.HasKey(si => si.SkuImageId);

        builder.HasOne(si => si.Sku)
            .WithMany(s => s.SkuImages)
            .HasForeignKey(si => si.SkuId);

        builder.HasOne(si => si.File)
            .WithMany()
            .HasForeignKey(si => si.FileId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
