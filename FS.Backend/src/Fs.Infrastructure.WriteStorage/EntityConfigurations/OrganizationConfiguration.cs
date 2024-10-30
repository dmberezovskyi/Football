using Fs.Domain.Aggregates.OrganizationAggregate;
using Fs.Infrastructure.Storage.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fs.Infrastructure.Storage.EntityConfigurations
{
    internal class OrganizationConfiguration : BaseConfiguration<Organization>
    {
        public override void Configure(EntityTypeBuilder<Organization> builder)
        {
            base.Configure(builder);

            builder.ToTable("Organizations");

            builder.Property(x => x.Name).IsRequired().HasMaxLength(64);

            builder.HasAddress(x => x.Address);
            builder.HasIndex(x => x.Name)
                .IsUnique();
        }
    }
}