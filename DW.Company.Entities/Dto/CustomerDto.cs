using System;

namespace DW.Company.Entities.Dto
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public string Document { get; set; }
        public int ProductLinkType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
