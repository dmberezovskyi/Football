using System;
using System.Threading;
using System.Threading.Tasks;
using Fs.Application.Commands.UserCommands;
using Fs.Application.Queries.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fs.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsersController : ApiControllerBase
    {
        [HttpPut("api/users/{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, UpdateUserCommand cmd, CancellationToken cancellationToken)
        {
            cmd.Id = id;
            await Mediator.Send(cmd, cancellationToken);
            return Ok();
        }

        [HttpGet("api/users")]
        public async Task<IActionResult> GetAllAsync([FromQuery] UsersQuery query, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(query, cancellationToken));
        }
    }
}
