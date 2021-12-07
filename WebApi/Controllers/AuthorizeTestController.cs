using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizeTestController : ControllerBase
    {
        #region public

        [HttpPost("authorized")]
        public IActionResult TestAuthorize()
        {
            return Ok();
        }

        [HttpPost("anonymous")]
        [AllowAnonymous]
        public IActionResult TestAnonymous()
        {
            return Ok();
        }
        #endregion
    }
}
