using System;
using System.Collections.Generic;

namespace DW.Company.Entities.Entity
{
    public class Product
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int? DesignerId { get; set; }
        public int? FileItemId { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Designer Designer { get; set; }
        public virtual FileItem FileItem { get; set; }
        public ICollection<ProductFile> Files { get; set; }
        public ICollection<ProductCategory> Categories { get; set; }
    }
}
