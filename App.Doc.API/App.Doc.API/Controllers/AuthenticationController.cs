using App.Doc.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace App.Doc.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private ITokenService tokenService;

        public AuthenticationController(ITokenService tokenService)
        {
            this.tokenService = tokenService;
        }

        [HttpGet]
        [Route("RequestToken")]
        public async Task<IActionResult> RequestToken()
        {
            var tokenResponse = await tokenService.GetToken("App.Doc.API.Read");
            return Ok(tokenResponse.AccessToken);
        }
    }
}
