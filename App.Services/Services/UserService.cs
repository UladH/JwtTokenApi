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
        private IAppSignInManager signInManager;
        private IJwtService jwtService;
        private IMapper mapper;

        #region constructor

        public UserService(IServiceProvider provider)
        {
            userManager = provider.GetService<IAppUserManager>();
            signInManager = provider.GetService<IAppSignInManager>();
            jwtService = provider.GetService<IJwtService>();
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

        public async Task<TokenPairModel> Login(UserLoginModel userLoginModel)
        {
            var user = await userManager.FindByEmailAsync(userLoginModel.Login);

            if (user == null)
            {
                user = await userManager.FindByNameAsync(userLoginModel.Login);
            }

            if (user == null)
            {
                throw new KeyNotFoundException("Invalid login or password");
            }

            var result = await signInManager.CheckPasswordSignInAsync(user, userLoginModel.Password, false);

            if (!result.Succeeded)
            {
                throw new KeyNotFoundException("Invalid login or password");
            }

            var userForJwtModel = mapper.Map<User, UserForJwtModel>(user);
            var tokenModel = await jwtService.CreateTokenPair(userForJwtModel);

            return tokenModel;
        }

        #endregion
    }
}
