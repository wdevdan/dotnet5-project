using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DW.Company.Entities.Entity;

namespace DW.Company.Data.Configurations
{
    public class DesignerConfiguration : IEntityTypeConfiguration<Designer>
    {
        public void Configure(EntityTypeBuilder<Designer> builder)
        {
            builder.ToTable("designer");
            builder.HasKey(k => k.Id);
            builder.Property(p => p.Id).HasColumnName("id").IsRequired();
            builder.Property(p => p.Name).HasColumnName("name").HasMaxLength(50).IsRequired();
            builder.Property(p => p.Surname).HasColumnName("surname").HasMaxLength(50).IsRequired();
            builder.Property(p => p.FileItemId).HasColumnName("file_item_id");
            builder.Property(p => p.CreatedAt).HasColumnName("created").HasMaxLength(50);
            builder.Property(p => p.UpdatedAt).HasColumnName("updated").HasMaxLength(50);

            builder.HasOne(o => o.FileItem)
                .WithMany()
                .HasForeignKey(f => f.FileItemId);
        }
    }
}