namespace DW.Company.Entities.Entity
{
    public class ProductFile
    {
        public int Id { get; set; }
        public int Order { get; set; }
        public int ProductId { get; set; }
        public int FileItemId { get; set; }

        public FileItem FileItem { get; set; }
        public Product Product { get; set; }
    }
}
