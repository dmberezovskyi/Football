using System;
using System.Threading;
using System.Threading.Tasks;
using Fs.Application.Commands.UserCommands;
using Microsoft.AspNetCore.Mvc;

namespace Fs.Api.Controllers
{
    public class RegistrationsController : ApiControllerBase
    {
        [HttpPost("api/registrations")]
        public async Task<IActionResult> RegisterAsync(RegisterUserCommand cmd, CancellationToken cancellationToken)
        {
            cmd.Id = Guid.NewGuid();
            await Mediator.Send(cmd, cancellationToken);
            return Ok();
        }
    }
}
