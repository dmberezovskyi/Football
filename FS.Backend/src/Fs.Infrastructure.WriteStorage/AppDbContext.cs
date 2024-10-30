using System;
using System.Threading;
using System.Threading.Tasks;
using Fs.Domain.Aggregates.EmailAggregate;
using Fs.Domain.Aggregates.UserAggregate;
using Fs.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Fs.Infrastructure.Storage
{
    public class AppDbContext : DbContext
    {
        public bool IsWorkStarted { get; set; }

        public AppDbContext() { }
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Version = 1;
                        entry.Entity.CreatedOn = entry.Entity.UpdatedOn = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.Version++;
                        entry.Entity.UpdatedOn = DateTime.UtcNow;
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
