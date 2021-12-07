using AppConfiguration;
using AppDbContext;
using Domain.Contracts.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;

namespace AppDependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        #region public 

        public static void AddDataAccessServices(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext.AppDbContext>();
            services.AddScoped<IAppDbContext>(sp => sp.GetRequiredService<AppDbContext.AppDbContext>());
        }

        public static void AddIdentityServices(this IServiceCollection services)
        {
            services.AddIdentity<User, UserRole>(cfg =>
            {
                cfg.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<AppDbContext.AppDbContext>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddCookie()
                .AddJwtBearer();
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
