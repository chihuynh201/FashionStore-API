using FashionStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionStore.Infrastructure.Persistence.Configurations;

public class SkuAttributeValueConfiguration : IEntityTypeConfiguration<SkuAttributeValue>
{
    public void Configure(EntityTypeBuilder<SkuAttributeValue> builder)
    {
        builder.HasKey(sav => sav.SkuAttributeValueId);

        builder.HasOne(sav => sav.Sku)
            .WithMany(s => s.SkuAttributeValues)
            .HasForeignKey(sav => sav.SkuId);

        builder.HasOne(sav => sav.AttributeValue)
            .WithMany(av => av.SkuAttributeValues)
            .HasForeignKey(sav => sav.AttributeValueId);
    }
}
