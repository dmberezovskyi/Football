using System;
using System.Linq;
using System.Threading.Tasks;
using Fs.Application.Permissions.User.Pep;
using Fs.Domain.Aggregates.UserAggregate;
using Fs.Infrastructure.Permissions;
using Fs.Infrastructure.Permissions.Abstractions;

namespace Fs.Application.Permissions.User.Pdp
{
    internal sealed class ListUsersPolicyDecisionPoint
    {
        public async Task<Decision> EvaluateDecisionAsync(BaseEnforcementSubject subject, EnforceListUsersResource resource)
        {
            switch (subject.UserRole)
            {
                case UserRole.Admin:
                    return IsPermittedAdminParts(resource.Parts)
                        ? Decision.Permit
                        : Decision.Deny;
                case UserRole.Trainer:
                    throw new ActionDeniedException();
                case UserRole.Player:
                    return IsPermittedPlayerParts(subject.UserId, resource.Ids, resource.Parts)
                        ? Decision.Permit
                        : Decision.Deny;
                case UserRole.Director:
                    return Decision.Deny;
                default:
                    return Decision.Deny;
            }
        }

        private static bool IsPermittedAdminParts(string[] parts)
        {
            return !parts.Except(new[]
            {
                Constants.User.Query.Parts.Snippet,
                Constants.User.Query.Parts.Profile,
                Constants.User.Query.Parts.Team,
                Constants.User.Query.Parts.Organization,
                Constants.User.Query.Parts.UserInfo,
                Constants.User.Query.Parts.UserInfoDetailed
            }).Any();
        }
        private static bool IsPermittedPlayerParts(Guid userId, Guid[] resourceIds, string[] parts)
        {
            var ids = resourceIds?.Distinct().ToArray();
            string[] allowedParts;

            if (ids != null && ids.Length == 1 && ids[0] == userId)
                allowedParts = new[]
                {
                    Constants.User.Query.Parts.Snippet,
                    Constants.User.Query.Parts.Profile,
                    Constants.User.Query.Parts.Team,
                    Constants.User.Query.Parts.Organization,
                    Constants.User.Query.Parts.UserInfo
                };
            else
                allowedParts = new[]
                {
                    Constants.User.Query.Parts.Snippet,
                    Constants.User.Query.Parts.Team,
                    Constants.User.Query.Parts.Organization
                };

            return !parts.Except(allowedParts).Any();
        }
    }
}