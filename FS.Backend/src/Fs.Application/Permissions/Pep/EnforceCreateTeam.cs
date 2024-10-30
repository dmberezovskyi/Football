using System;
using Fs.Infrastructure.Permissions.Abstractions;

namespace Fs.Application.Permissions.Pep
{
    public sealed class EnforceCreateTeam :
        BasePolicyEnforcementRequest<BaseEnforcementSubject, CreateTeamResource>
    {
    }

    public sealed class CreateTeamResource
    {
        public Guid? OrganizationId { get; }

        public CreateTeamResource(Guid? organizationId)
        {
            OrganizationId = organizationId;
        }
    }
}
