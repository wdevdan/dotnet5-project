using Microsoft.Extensions.Configuration;

using DW.Company.Contracts.Settings;

namespace DW.Company.Services.Settings
{
    public class EnvironmentSettings : IEnvironmentSettings
    {
        private readonly IConfiguration _configuration;

        public EnvironmentSettings(IConfiguration configuration)
        {
            _configuration = configuration;

        }

        private IConfigurationSection GetEnvironmentSection() => _configuration.GetSection("Environment");

        private string GetImagesDirectory() => GetEnvironmentSection().GetSection("ImagesDirectory").Value;
        private string GetVideosDirectory() => GetEnvironmentSection().GetSection("VideosDirectory").Value;
        private string GetImagesExtensions() => GetEnvironmentSection().GetSection("ImagesExtensions").Value;
        private string GetVideosExtensions() => GetEnvironmentSection().GetSection("VideosExtensions").Value;

        public string IMAGESDIRECTORY => GetImagesDirectory();
        public string VIDEOSDIRECTORY => GetVideosDirectory();
        public string[] IMAGESEXTENSIONS => GetImagesExtensions().Split(',');
        public string[] VIDEOSEXTENSIONS => GetVideosExtensions().Split(',');
    }

}
