using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using DW.Company.Entities.Entity;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DW.Company.Contracts.Data
{
    public interface IDBContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<Leather> Leathers { get; set; }
        DbSet<Customer> Customers { get; set; }
        DbSet<CustomerProduct> CustomerProducts { get; set; }
        DbSet<Designer> Designers { get; set; }
        DbSet<FileItem> FileItems { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<LeatherType> LeatherTypes { get; set; }
        DbSet<ItemVersion> ItemVersions { get; set; }
        DbSet<ProductFile> ProductFiles { get; set; }
        DbSet<ProductCategory> ProductCategories { get; set; }

        IQueryable<User> UsersSession { get; }
        IQueryable<Customer> CustomersSession { get; }
        DatabaseFacade Database { get; }
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        EntityEntry<TEntity> Entry<TEntity>([NotNull] TEntity entity) where TEntity : class;
    }
}
