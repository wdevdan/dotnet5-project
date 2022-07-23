using System;

namespace DW.Company.Contracts.Settings
{
    public interface ISessionSettings
    {
        int? UserId { get; set; }
        string Role { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Login { get; set; }
        string Email { get; set; }
        DateTime ValidUntil { get; set; }
        void Clear();
    }
}
