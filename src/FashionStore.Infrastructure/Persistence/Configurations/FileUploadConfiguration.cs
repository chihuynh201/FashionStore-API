using FashionStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionStore.Infrastructure.Persistence.Configurations;
public class FileUploadConfiguration : IEntityTypeConfiguration<FileUpload>
{
    public void Configure(EntityTypeBuilder<FileUpload> builder)
    {
        builder.HasKey(f => f.FileId);
        builder.Property(f => f.FileName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(f => f.Url)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(f => f.ContentType)
            .IsRequired(false)
            .IsUnicode(false)
            .HasMaxLength(50);

        builder.Property(f => f.Status)
            .IsRequired(false)
            .HasMaxLength(20);
    }
}