using FashionStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionStore.Infrastructure.Persistence.Configurations;

public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
{
    public void Configure(EntityTypeBuilder<OrderDetail> builder)
    {
        builder.HasOne(od => od.Order)
            .WithMany(o => o.OrderDetails)
            .HasForeignKey(od => od.OrderId);

        builder.HasOne(od => od.Sku)
            .WithMany(s => s.OrderDetails)
            .HasForeignKey(od => od.SkuId);

        builder.Property(od => od.UnitPrice)
            .HasColumnType("decimal(18,2)");

        builder.Property(od => od.TotalPrice)
            .HasColumnType("decimal(18,2)");

    }
}
