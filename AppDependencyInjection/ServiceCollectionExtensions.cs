using App.Contracts.Interfaces;
using App.Services.Services;
using AppConfiguration;
using AppDbContext;
using AppMapper;
using Domain.Contracts.Interfaces.Identity;
using Domain.Contracts.Models;
using Domain.Repositories.IdentityManagers;
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
                .AddEntityFrameworkStores<AppDbContext.AppDbContext>()
                .AddUserManager<AppUserManager>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddCookie()
                .AddJwtBearer();

            services.AddScoped<IAppUserManager>(sp => sp.GetRequiredService<AppUserManager>());
        }

        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IAppConfiguration, AppConfiguration.AppConfiguration>();
            services.AddSingleton(MapperBuilder.Create());
        }

        public static void AddDomainLayerServices(this IServiceCollection services)
        {
        }

        public static void AddAppLayerServices(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
        }

        #endregion
    }
}
