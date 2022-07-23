using Microsoft.EntityFrameworkCore;
using DW.Company.Data.Configurations;

namespace DW.Company.Data.Extensions
{
    public static class ModelBuilderExtension
    {
        public static void ApplyConfigurations(this ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new LeatherConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new DesignerConfiguration());
            modelBuilder.ApplyConfiguration(new FileItemConfiguration());
            modelBuilder.ApplyConfiguration(new ItemVersionConfiguration());
            modelBuilder.ApplyConfiguration(new LeatherTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ProductFileConfiguration());
            modelBuilder.ApplyConfiguration(new ProductCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerProductConfiguration());
        }
    }
}
