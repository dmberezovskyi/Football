using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fs.Application.Permissions.Abstractions;
using Fs.Infrastructure.Storage.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Fs.Application.Permissions
{
    internal class PolicyInformationPoint : IPolicyInformationPoint
    {
        private readonly IRepository _repository;

        public PolicyInformationPoint(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid?> GetUserOrganizationId(Guid userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();                                                                                                    
            //return await _repository.Get<Domain.Aggregates.UserAggregate.User>(x => x.Id == userId)
            //    .Select(x => x.OrganizationId)
            //    .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
