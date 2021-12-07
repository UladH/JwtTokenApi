using Domain.Contracts.Interfaces.Identity;
using Domain.Contracts.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Domain.Repositories.IdentityManagers
{
    public class AppSignInManager: SignInManager<User>, IAppSignInManager
    {
        #region constructor

        public AppSignInManager(AppUserManager userManager, IHttpContextAccessor contextAccessor, 
            IUserClaimsPrincipalFactory<User> claimsFactory, IOptions<IdentityOptions> optionsAccessor, 
            ILogger<AppSignInManager> logger, IAuthenticationSchemeProvider schemes, IUserConfirmation<User> confirmation)
            :base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
        {

        }

        #endregion

        #region public
        #endregion
    }
}
