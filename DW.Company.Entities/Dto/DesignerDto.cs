using System;

namespace DW.Company.Entities.Dto
{
    public class DesignerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int? FileItemId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual FileItemDto FileItem { get; set; }
    }
}
