using Microsoft.EntityFrameworkCore;
using DW.Company.Entities.Entity;
using System.Linq;

namespace DW.Company.Services.Extensions
{
    public static class IQuerybleExtensions
    {
        public static IQueryable<Product> Includes(this IQueryable<Product> query)
        {
            return query
                .Include("Categories.Category")
                .Include("Files.FileItem")
                .Include("FileItem")
                .Include("Designer");
        }

        public static IQueryable<Designer> Includes(this IQueryable<Designer> query)
        {
            return query
                .Include("FileItem");
        }

        public static IQueryable<Leather> Includes(this IQueryable<Leather> query)
        {
            return query
                .Include("FileItem")
                .Include("LeatherType");
        }

        public static IQueryable<CustomerProduct> Includes(this IQueryable<CustomerProduct> query)
        {
            return query
                .Include("Product.Files")
                .Include("Product.Files.FileItem");
        }
    }
}
