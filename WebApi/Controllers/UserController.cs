using App.Contracts.Interfaces;
using App.Contracts.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService userService;

        #region constructor

        public UserController(IServiceProvider provider)
        {
            userService = provider.GetService<IUserService>();
        }

        #endregion

        #region public

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserRegisterModel model)
        {
            var result = await userService.Register(model);
            return Ok(result);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginModel model)
        {
            var tokenModel = await userService.Login(model);
            return Ok(tokenModel);
        }

        #endregion
    }
}
