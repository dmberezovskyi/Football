using Fs.Infrastructure.ReadStorage.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fs.Infrastructure.ReadStorage.EntityConfigurations
{
    internal class OrganizationConfiguration : BaseConfiguration<OrganizationEntity>
    {
        public override void Configure(EntityTypeBuilder<OrganizationEntity> builder)
        {
            base.Configure(builder);

            builder.ToTable("Organizations");

            builder.OwnsOne(x => x.Address);
        }
    }
}