using System;

namespace Fs.Domain.SeedWork
{
    public abstract class BaseDomainEvent : IDomainEvent
    {
        public Guid Id { get; set; }
    }
}