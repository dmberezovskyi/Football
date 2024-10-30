using System;
using System.Threading;
using System.Threading.Tasks;
using Fs.Domain.Exceptions.Organization;
using Fs.Domain.SeedWork;
using Fs.Domain.Services;
using Fs.Domain.Values;

namespace Fs.Domain.Aggregates.OrganizationAggregate
{
    public class Organization : BaseEntity,
        IAggregateRoot
    {
        public string Name { get; private set; }
        public Address Address { get; private set; }

        private Organization(Guid id)
            : base(id) { }
        protected Organization() { }
        public static async Task<Organization> CreateAsync(Guid id, string name, Address address, IOrganizationService organizationService, CancellationToken cancellationToken)
        {
            var org = new Organization(id);
            await org.UpdateNameAsync(name, organizationService, cancellationToken);
            org.UpdateAddress(address);
            return org;
        }

        public async Task UpdateNameAsync(string name, IOrganizationService organizationService, CancellationToken cancellationToken)
        {
            if (await organizationService.IsNameExistsAsync(name, cancellationToken))
                throw new OrganizationNameAlreadyExistsException();

            Name = name.Trim();
        }
        public void UpdateAddress(Address address)
        {
            Address = address;
        }
    }
}
