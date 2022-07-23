using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DW.Company.Entities.Entity;

namespace DW.Company.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("product");
            builder.HasKey(k => k.Id);
            builder.Property(p => p.Id).HasColumnName("id").IsRequired();
            builder.Property(p => p.Code).HasColumnName("code").HasMaxLength(50).IsRequired();
            builder.Property(p => p.Name).HasColumnName("name").HasMaxLength(50).IsRequired();
            builder.Property(p => p.Description).HasColumnName("description").IsRequired();
            builder.Property(p => p.FileItemId).HasColumnName("file_item_id");
            builder.Property(p => p.DesignerId).HasColumnName("designer_id");
            builder.Property(p => p.CreatedAt).HasColumnName("created");
            builder.Property(p => p.UpdatedAt).HasColumnName("updated");

            builder.HasOne(o => o.Designer)
                .WithMany()
                .HasForeignKey(k => k.DesignerId);

            builder.HasOne(o => o.FileItem)
                .WithMany()
                .HasForeignKey(k => k.FileItemId);
        }
    }
}
