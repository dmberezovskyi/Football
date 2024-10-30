using Fs.Domain.Aggregates.OrganizationAggregate;
using Fs.Domain.Aggregates.TeamAggregate;
using Fs.Domain.Aggregates.UserAggregate;
using Fs.Infrastructure.Storage.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fs.Infrastructure.Storage.EntityConfigurations
{
    internal class UserConfiguration : BaseConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.ToTable("Users");

            builder.Property(x => x.Email).IsRequired().HasMaxLength(64);
            builder.Property(x => x.PasswordHash).IsRequired().HasMaxLength(64);
            builder.Property(x => x.PasswordSalt).IsRequired().HasMaxLength(64);

            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.Role).IsRequired();

            builder.Property(x => x.EmailConfirmed).IsRequired();
            builder.Property(x => x.EmailConfirmationToken).IsRequired().HasMaxLength(64);

            builder.OwnsOne(x => x.Profile, profileBuilder =>
            {
                profileBuilder.Property(x => x.FirstName).IsRequired().HasMaxLength(64);
                profileBuilder.Property(x => x.MiddleName).HasMaxLength(64);
                profileBuilder.Property(x => x.LastName).IsRequired().HasMaxLength(64);
                profileBuilder.Property(x => x.About).IsRequired(false).HasMaxLength(256);
                profileBuilder.Property(x => x.BirthDate).IsRequired();
                profileBuilder.Property(x => x.Phone).IsRequired().HasMaxLength(16);
                profileBuilder.HasAddress(x => x.Address);
            });

            builder.HasOne<Team>()
                .WithMany()
                .IsRequired(false)
                .HasForeignKey(x => x.TeamId);

            builder.HasOne<Organization>()
                .WithMany()
                .IsRequired(false)
                .HasForeignKey(x => x.OrganizationId);

            builder.HasIndex(x => x.Status);
            builder.HasIndex(x => x.CreatedOn);
            builder.HasIndex(x => x.Email)
                .IsUnique();
        }
    }
}