using FashionStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionStore.Infrastructure.Persistence.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>

{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.Property(p => p.CustomerName)
            .IsRequired();

        builder.Property(p => p.Phone)
            .IsRequired()
            .HasMaxLength(20);
    }
}
