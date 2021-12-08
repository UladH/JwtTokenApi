using App.Contracts.Interfaces;
using App.Services.Services;
using AppConfiguration;
using AppConfiguration.Constants;
using AppDbContext;
using AppMapper;
using Domain.Contracts.Interfaces.Identity;
using Domain.Contracts.Interfaces.Repositories;
using Domain.Contracts.Models;
using Domain.Repositories.IdentityManagers;
using Domain.Repositories.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

        public static void AddIdentityServices(this IServiceCollection services, IAppConfiguration configuration)
        {
            services.AddIdentity<User, UserRole>(cfg =>
            {
                cfg.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<AppDbContext.AppDbContext>()
                .AddUserManager<AppUserManager>()
                .AddSignInManager<AppSignInManager>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddCookie()
                .AddJwtBearer(cfg => {
                    cfg.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration.Get(TokenKeys.Issuer),
                        ValidAudience = configuration.Get(TokenKeys.Audience),
                        LifetimeValidator = (DateTime? notBefore, DateTime? expires, SecurityToken tokenToValidate, TokenValidationParameters @param) => {
                            return expires == null ? false : expires > DateTime.UtcNow;
                        },
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.Get(TokenKeys.Key)))
                    };
                });

            services.AddScoped<IAppUserManager>(sp => sp.GetRequiredService<AppUserManager>());
            services.AddScoped<IAppSignInManager>(sp => sp.GetRequiredService<AppSignInManager>());
        }

        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IAppConfiguration, AppConfiguration.AppConfiguration>();
            services.AddSingleton(MapperBuilder.Create());
        }

        public static void AddDomainLayerServices(this IServiceCollection services)
        {
            services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddTransient<IRepositoryManager, RepositoryManager>();
        }

        public static void AddAppLayerServices(this IServiceCollection services)
        {
            services.AddTransient<IJwtService, JwtService>();
            services.AddTransient<IUserService, UserService>();
        }

        #endregion
    }
}
