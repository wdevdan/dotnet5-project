namespace DW.Company.Entities.Dto
{
    public class CustomerProductDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public virtual ProductDto Product { get; set; }
    }
}
