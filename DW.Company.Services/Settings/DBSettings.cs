using Microsoft.Extensions.Configuration;

using DW.Company.Contracts.Settings;

namespace DW.Company.Services.Settings
{
    public class DBSettings : IDBSettings
    {
        private readonly IConfiguration _configuration;

        public DBSettings(IConfiguration configuration)
        {
            _configuration = configuration;

        }

        private IConfigurationSection GetDatabaseSection()
        {
            return _configuration.GetSection("Database");
        }

        private string GetDatabaseSchema() => GetDatabaseSection().GetSection("Schema").Value;
        public string DATABASECONNECTIONSTRING => _configuration.GetConnectionString("DefaultConnection");
        public string DATABASESCHEMA => GetDatabaseSchema();
    }
}
