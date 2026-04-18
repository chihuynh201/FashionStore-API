using FashionStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionStore.Infrastructure.Persistence.Configurations;

public class CategoryAttributeConfiguration : IEntityTypeConfiguration<CategoryAttribute>
{
    public void Configure(EntityTypeBuilder<CategoryAttribute> builder)
    {
        builder.HasKey(ca => ca.CategoryAttributeId);

        builder.HasOne(ca => ca.Category)
            .WithMany(c => c.CategoryAttributes)
            .HasForeignKey(ca => ca.CategoryId);

        builder.HasOne(ca => ca.ProductAttribute)
            .WithMany(a => a.CategoryAttributes)
            .HasForeignKey(ca => ca.ProductAttributeId);
    }
}
