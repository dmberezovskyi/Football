using System.Threading;
using System.Threading.Tasks;
using Fs.Api.Models;
using Fs.Application.Commands.AuthCommands;
using IdentityServer4;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Fs.Api.Controllers
{
    public class AuthController : ApiControllerBase
    {
        [HttpPost("api/auth/login")]
        public async Task<IActionResult> LoginAsync(LogInCommand cmd, CancellationToken cancellationToken)
        {
            var response = await Mediator.Send(cmd, cancellationToken);

            await HttpContext.SignInAsync(response.Principal);

            return Ok(new LoginViewModel
            {
                RedirectUrl = response.RedirectUrl
            });
        }

        [HttpPost("api/auth/logout")]
        public async Task LogoutAsync([FromQuery] LogOutCommand cmd, CancellationToken cancellationToken)
        {
            await Mediator.Send(cmd, cancellationToken);
            await HttpContext.SignOutAsync(IdentityServerConstants.DefaultCookieAuthenticationScheme);
        }
    }
}