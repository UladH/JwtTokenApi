using App.Contracts.Interfaces;
using App.Contracts.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JwtController : ControllerBase
    {
        private IJwtService jwtService;

        #region constructor

        public JwtController(IServiceProvider provider)
        {
            jwtService = provider.GetService<IJwtService>();
        }

        #endregion

        #region public

        [HttpPost("refreshToken")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RefreshTokenModel model)
        {
            var result = jwtService.CreateTokenPairByRefreshToken(model);
            return Ok(result);
        }

        #endregion
    }
}
