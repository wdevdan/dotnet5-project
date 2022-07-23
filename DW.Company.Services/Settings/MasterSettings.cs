using Microsoft.Extensions.Configuration;

using DW.Company.Contracts.Settings;

namespace DW.Company.Services.Settings
{
    public class MasterSettings : IMasterSettings
    {
        private readonly IConfiguration _configuration;

        public MasterSettings(IConfiguration configuration)
        {
            _configuration = configuration;

        }

        private IConfigurationSection GetMasterUserSection() => _configuration.GetSection("MasterUser");

        private string GetMasterUserName() => GetMasterUserSection().GetSection("User").Value;
        private string GetMasterUserPassword() => GetMasterUserSection().GetSection("Password").Value;
        private string GetMasterUserEmail() => GetMasterUserSection().GetSection("Email").Value;

        public string MASTERUSER => GetMasterUserName();
        public string MASTERPASSWORD => GetMasterUserPassword();
        public string MASTEREMAIL => GetMasterUserEmail();
    }

}
