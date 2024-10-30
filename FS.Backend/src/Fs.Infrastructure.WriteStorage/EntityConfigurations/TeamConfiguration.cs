using Fs.Domain.Aggregates.OrganizationAggregate;
using Fs.Domain.Aggregates.TeamAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fs.Infrastructure.Storage.EntityConfigurations
{
    internal class TeamConfiguration : BaseConfiguration<Team>
    {
        public override void Configure(EntityTypeBuilder<Team> builder)
        {
            base.Configure(builder);

            builder.ToTable("Teams");

            builder.Property(x => x.Name).IsRequired().HasMaxLength(64);

            builder.HasOne<Organization>()
                .WithMany()
                .IsRequired(false)
                .HasForeignKey(x => x.OrganizationId);

            builder.HasIndex(x => x.Name);
        }
    }
}