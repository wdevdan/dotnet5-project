using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DW.Company.Entities.Entity;

namespace DW.Company.Data.Configurations
{
    public class LeatherConfiguration : IEntityTypeConfiguration<Leather>
    {
        public void Configure(EntityTypeBuilder<Leather> builder)
        {
            builder.ToTable("leather");
            builder.HasKey(k => k.Id);
            builder.Property(p => p.Id).HasColumnName("id").IsRequired();
            builder.Property(p => p.FileItemId).HasColumnName("file_item_id");
            builder.Property(p => p.LeatherTypeId).HasColumnName("leather_type_id").IsRequired();
            builder.Property(p => p.Code).HasColumnName("code").HasMaxLength(50).IsRequired();
            builder.Property(p => p.Name).HasColumnName("name").HasMaxLength(50).IsRequired();

            builder.HasOne(o => o.FileItem)
                .WithMany()
                .HasForeignKey(f => f.FileItemId);

            builder.HasOne(o => o.LeatherType)
                .WithMany()
                .HasForeignKey(f => f.LeatherTypeId);
        }
    }
}