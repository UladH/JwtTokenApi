using AppConfiguration;
using Microsoft.Extensions.Hosting;

namespace AppDependencyInjection
{
    public static class StartUpServices
    {
        public static IAppConfiguration GetConfiguration(IHostEnvironment environment)
        {
            return new AppConfiguration.AppConfiguration(environment);
        }
    }
}
