namespace DW.Company.Contracts.Settings
{
    public interface IEnvironmentSettings
    {
        string IMAGESDIRECTORY { get; }
        string VIDEOSDIRECTORY { get; }
        string[] VIDEOSEXTENSIONS { get; }
        string[] IMAGESEXTENSIONS { get; }
    }
}
