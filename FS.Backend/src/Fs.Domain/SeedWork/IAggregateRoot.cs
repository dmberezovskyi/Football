using System;

namespace Fs.Domain.SeedWork
{
    public interface IAggregateRoot
    {
        Guid Id { get; }
        int Version { get; }
    }
}
