using DW.Company.Entities.Entity;

namespace DW.Company.Entities.Dto
{
    public class ProductCategoryDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
