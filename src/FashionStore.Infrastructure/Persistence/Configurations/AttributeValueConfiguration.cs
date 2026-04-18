using FashionStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionStore.Infrastructure.Persistence.Configurations;

public class AttributeValueConfiguration : IEntityTypeConfiguration<AttributeValue>
{
    public void Configure(EntityTypeBuilder<AttributeValue> builder)
    {
        builder.HasKey(av => av.AttributeValueId);

        builder.Property(av => av.Value)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(av => av.DisplayOrder)
            .HasDefaultValue(0);

        builder.HasOne(av => av.ProductAttribute)
            .WithMany(a => a.AttributeValues)
            .HasForeignKey(av => av.ProductAttributeId);
    }
}
