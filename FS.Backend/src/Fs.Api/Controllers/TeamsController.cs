using System;
using System.Threading;
using System.Threading.Tasks;
using Fs.Application.Commands.TeamCommands;
using Microsoft.AspNetCore.Mvc;

namespace Fs.Api.Controllers
{
    public class TeamsController : ApiControllerBase
    {
        [HttpPost("api/teams")]
        public async Task<IActionResult> CreateAsync(CreateTeamCommand cmd, CancellationToken cancellationToken)
        {
            cmd.Id = Guid.NewGuid();
            await Mediator.Send(cmd, cancellationToken);

            return Created($"api/teams?ids={cmd.Id}", new
            {
                id = cmd.Id
            });
        }
    }
}
