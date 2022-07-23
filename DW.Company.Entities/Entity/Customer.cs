using System;
using System.Collections.Generic;

namespace DW.Company.Entities.Entity
{
    public class Customer
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
