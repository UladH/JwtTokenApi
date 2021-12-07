using AppExceptions.Identity;
using Domain.Contracts.Interfaces.Identity;
using Domain.Contracts.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Domain.Repositories.IdentityManagers
{
    public class AppUserManager: UserManager<User>, IAppUserManager
    {
        #region constructor

        public AppUserManager(IUserStore<User> store, IOptions<IdentityOptions> optionsAccessor, 
            IPasswordHasher<User> passwordHasher, IEnumerable<IUserValidator<User>> userValidators, 
            IEnumerable<IPasswordValidator<User>> passwordValidators, ILookupNormalizer keyNormalizer, 
            IdentityErrorDescriber errors, IServiceProvider services, ILogger<AppUserManager> logger)
            :base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, 
                 errors, services, logger)
        {
        }

        #endregion

        #region public

        public override async Task<IdentityResult> CreateAsync(User user, string password) { 
            var result = await base.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                throw new IdentityException(result);
            }

            return result;
        }

        #endregion

        #region private
        #endregion
    }
}
