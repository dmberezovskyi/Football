using System;
using System.Threading;
using System.Threading.Tasks;
using Fs.Application.Commands.OrganizationCommands;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fs.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrganizationsController : ApiControllerBase
    {
        [HttpPost("api/organizations")]
        public async Task<IActionResult> CreateAsync(CreateOrganizationCommand cmd, CancellationToken cancellationToken)
        {
            cmd.Id = Guid.NewGuid();
            await Mediator.Send(cmd, cancellationToken);

            return Created($"api/organizations?ids={cmd.Id}", new
            {
                id = cmd.Id
            });
        }
    }
}
