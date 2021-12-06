using AppConfiguration;
using Microsoft.Extensions.DependencyInjection;

namespace AppDependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        #region public 

        public static void AddDataAccessServices(this IServiceCollection services)
        {
        }

        public static void AddIdentityServices(this IServiceCollection services)
        {
        }

        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IAppConfiguration, AppConfiguration.AppConfiguration>();
        }

        public static void AddDomainLayerServices(this IServiceCollection services)
        {
        }

        public static void AddAppLayerServices(this IServiceCollection services)
        {
        }
        #endregion
    }
}
