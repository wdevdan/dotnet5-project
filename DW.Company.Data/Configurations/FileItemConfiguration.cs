using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DW.Company.Entities.Entity;

namespace DW.Company.Data.Configurations
{
    public class FileItemConfiguration : IEntityTypeConfiguration<FileItem>
    {
        public void Configure(EntityTypeBuilder<FileItem> builder)
        {
            builder.ToTable("file_item");
            builder.HasKey(k => k.Id);
            builder.Property(p => p.Id).HasColumnName("id").IsRequired();
            builder.Property(p => p.CurrentName).HasColumnName("current_name").HasMaxLength(100).IsRequired();
            builder.Property(p => p.OriginName).HasColumnName("origin_name").HasMaxLength(100).IsRequired();
            builder.Property(p => p.Extension).HasColumnName("extension").HasMaxLength(10).IsRequired();
            builder.Property(p => p.Path).HasColumnName("path").HasMaxLength(254).IsRequired();
            builder.Property(p => p.Md5).HasColumnName("md5").HasMaxLength(36).IsRequired();
            builder.Property(p => p.CreatedAt).HasColumnName("created");
            builder.Property(p => p.UpdatedAt).HasColumnName("updated");
        }
    }
}
