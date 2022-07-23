namespace DW.Company.Entities.Dto
{
    public class ProductFileDto
    {
        public int Id { get; set; }
        public int Order { get; set; }
        public int FileItemId { get; set; }
        public int ProductId { get; set; }

        public FileItemDto FileItem { get; set; }
    }
}
