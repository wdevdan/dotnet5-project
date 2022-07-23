using Microsoft.EntityFrameworkCore;
using DW.Company.Common;
using DW.Company.Contracts.Data;
using DW.Company.Contracts.Settings;
using DW.Company.Data.Extensions;
using DW.Company.Entities.Entity;
using System.Linq;

namespace DW.Company.Data
{
    public class DBContext : DbContext, IDBContext
    {
        private readonly IDBSettings _dbSettings;
        private readonly ISessionSettings _sessionSettings;

        public DBContext(IDBSettings dbSettings, ISessionSettings sessionSettings)
        {
            _dbSettings = dbSettings;
            _sessionSettings = sessionSettings;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Leather> Leathers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerProduct> CustomerProducts { get; set; }
        public DbSet<Designer> Designers { get; set; }
        public DbSet<FileItem> FileItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<LeatherType> LeatherTypes { get; set; }
        public DbSet<ItemVersion> ItemVersions { get; set; }
        public DbSet<ProductFile> ProductFiles { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }

        public IQueryable<User> UsersSession
        {
            get
            {
                var _query = Users.AsQueryable();
                if (_sessionSettings.Role.Equals(Constants.MASTERROLE) || _sessionSettings.Role.Equals(Constants.MANAGERROLE))
                    return _query;
                else if (_sessionSettings.Role.Equals(Constants.CUSTOMERROLE))
                {
                    return _query
                        .Where(
                            w => Users.Any(
                                a => a.Id == _sessionSettings.UserId &&
                                a.CustomerId == w.CustomerId
                            ) && w.Role.Equals(Constants.CUSTOMERROLE)
                        );
                }
                return _query.Where(w => false);
            }
        }

        public IQueryable<Customer> CustomersSession
        {
            get
            {
                var _query = Customers.AsQueryable();
                if (_sessionSettings.Role.Equals(Constants.MASTERROLE) || _sessionSettings.Role.Equals(Constants.MANAGERROLE))
                    return _query;
                return _query
                    .Where(
                        w => Users.Any(
                            a => a.Id == _sessionSettings.UserId &&
                            a.CustomerId == w.Id
                        )
                    );
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_dbSettings.DATABASECONNECTIONSTRING);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(_dbSettings.DATABASESCHEMA);

            modelBuilder.ApplyConfigurations();

            base.OnModelCreating(modelBuilder);
        }
    }
}
