using System;

namespace DW.Company.Entities.Entity
{
    public class Designer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int? FileItemId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public virtual FileItem FileItem { get; set; }
    }
}