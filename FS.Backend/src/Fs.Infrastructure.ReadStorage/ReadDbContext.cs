using Fs.Infrastructure.ReadStorage.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fs.Infrastructure.ReadStorage
{
    public class ReadDbContext : DbContext
    {
        public virtual DbSet<UserEntity> Users { get; set; }

        public ReadDbContext() { }
        public ReadDbContext(DbContextOptions<ReadDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
