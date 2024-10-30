using Fs.Infrastructure.ReadStorage.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fs.Infrastructure.ReadStorage.EntityConfigurations
{
    internal class TeamConfiguration : BaseConfiguration<TeamEntity>
    {
        public override void Configure(EntityTypeBuilder<TeamEntity> builder)
        {
            base.Configure(builder);

            builder.ToTable("Teams");
        }
    }
}