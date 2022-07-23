using System;
using System.Collections.Generic;

namespace DW.Company.Entities.Dto
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int? DesignerId { get; set; }
        public int? FileItemId { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual DesignerDto Designer { get; set; }
        public virtual FileItemDto FileItem { get; set; }
        public virtual ICollection<ProductFileDto> Files { get; set; }
        public virtual ICollection<ProductCategoryDto> Categories { get; set; }
    }
}