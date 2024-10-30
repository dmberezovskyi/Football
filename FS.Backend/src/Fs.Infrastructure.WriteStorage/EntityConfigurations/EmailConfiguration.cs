using Fs.Domain.Aggregates.EmailAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fs.Infrastructure.Storage.EntityConfigurations
{
    internal class EmailConfiguration : BaseConfiguration<Email>
    {
        public override void Configure(EntityTypeBuilder<Email> builder)
        {
            base.Configure(builder);

            builder.ToTable("Emails");

            builder.Property(x => x.Recipient).IsRequired().HasMaxLength(64);
            builder.Property(x => x.TemplateName).IsRequired().HasMaxLength(64);
            builder.Property(x => x.CultureName).IsRequired().HasMaxLength(8);
        }
    }
}