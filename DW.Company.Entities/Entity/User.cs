using System;

namespace DW.Company.Entities.Entity
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? CustomerId { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
        public DateTime ValidSince { get; set; }
        public DateTime ValidUntil { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
