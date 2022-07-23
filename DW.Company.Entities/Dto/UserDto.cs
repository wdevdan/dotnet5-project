using System;

namespace DW.Company.Entities.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? CustomerId { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public DateTime ValidSince { get; set; }
        public DateTime ValidUntil { get; set; }

        public virtual CustomerDto Customer { get; set; }
    }
}
