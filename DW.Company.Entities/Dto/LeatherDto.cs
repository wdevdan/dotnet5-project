namespace DW.Company.Entities.Dto
{
    public class LeatherDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int? FileItemId { get; set; }
        public int? LeatherTypeId { get; set; }

        public virtual FileItemDto FileItem { get; set; }
        public virtual LeatherTypeDto LeatherType { get; set; }
    }
}
