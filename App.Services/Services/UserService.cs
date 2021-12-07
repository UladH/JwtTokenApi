using App.Contracts.Interfaces;
using App.Contracts.Models;
using AutoMapper;
using Domain.Contracts.Interfaces.Identity;
using Domain.Contracts.Models;
using Microsoft.Extensions.DependencyInjection;

namespace App.Services.Services
{
    public class UserService: IUserService
    {
        private IAppUserManager userManager;
        private IMapper mapper;

        #region constructor

        public UserService(IServiceProvider provider)
        {
            userManager = provider.GetService<IAppUserManager>();
            mapper = provider.GetService<IMapper>();
        }

        #endregion

        #region public

        public async Task<bool> Register(UserRegisterModel registerModel)
        {
            var user = mapper.Map<UserRegisterModel, User>(registerModel);
            var password = registerModel.Password;
            var result = await userManager.CreateAsync(user, password);

            return result.Succeeded;
        }

        #endregion
    }
}
