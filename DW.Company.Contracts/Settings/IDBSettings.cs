namespace DW.Company.Contracts.Settings
{
    public interface IDBSettings
    {
        string DATABASECONNECTIONSTRING { get; }
        string DATABASESCHEMA { get; }
    }
}
