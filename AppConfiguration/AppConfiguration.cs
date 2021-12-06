using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace AppConfiguration
{
    public class AppConfiguration: IAppConfiguration
    {
        private readonly string relativePath = "Configurations/";
        private readonly string configNameFormat = "appsettings.{0}.json";
        private readonly string releaseName = "Release";

        private IConfiguration configuration;
        private IHostEnvironment environment;

        #region constructor

        public AppConfiguration(IHostEnvironment environment)
        {
            this.environment = environment;
            configuration = BuildConfiguration();
        }

        #endregion

        #region public

        public string Get(string key)
        {
            return Get<string>(key);
        }

        public T Get<T>(string key)
        {
            return configuration.GetValue<T>(key);
        }

        #endregion

        #region private

        private IConfiguration BuildConfiguration()
        {
            var builder = new ConfigurationBuilder();
            var currentPath = Directory.GetCurrentDirectory();
            var releaseConfigurationPath = Path.Combine(currentPath, relativePath, string.Format(configNameFormat, releaseName));
            var environmentConfigurationPath = Path.Combine(currentPath, relativePath,
                string.Format(configNameFormat, environment.EnvironmentName));

            builder.AddJsonFile(releaseConfigurationPath);
            builder.AddJsonFile(environmentConfigurationPath);

            return builder.Build();
        }

        #endregion
    }
}
