using DW.Company.Contracts.Settings;
using System;

namespace DW.Company.Services.Settings
{
    public class SessionSettings : ISessionSettings
    {
        public int? UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public DateTime ValidUntil { get; set; }

        public void Clear()
        {
            UserId = null;
            FirstName = null;
            LastName = null;
            Login = null;
            Email = null;
            Role = null;
            ValidUntil = DateTime.MinValue;
        }
    }
}
