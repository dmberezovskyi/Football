using System;

namespace Fs.Infrastructure.ReadStorage.Entities
{
    public abstract record BaseEntity
    {
        public Guid Id { get; init; }
        public DateTime CreatedOn { get; init; }
        public DateTime UpdatedOn { get; init; }
        public int Version { get; init; }
    }
}
