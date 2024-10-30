using Fs.Infrastructure.ReadStorage.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fs.Infrastructure.ReadStorage.EntityConfigurations
{
    internal class UserConfiguration : BaseConfiguration<UserEntity>
    {
        public override void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            base.Configure(builder);

            builder.ToTable("Users");

            builder.OwnsOne(x => x.Profile, profileBuilder =>
            {
                profileBuilder.OwnsOne(x => x.Address);
            });
        }
    }
}