using System;
using System.Collections.Generic;
using MediatR;

namespace Fs.Domain.SeedWork
{
    public abstract class BaseEntity
    {
        public Guid Id { get; private set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int Version { get; set; }

        private List<INotification> _domainEvents;
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

        protected BaseEntity(Guid id)
        {
            Id = id;
        }
        protected BaseEntity()
        {
        }

        protected void RaiseEvent(IDomainEvent evt)
        {
            evt.Id = Id;

            _domainEvents = _domainEvents ?? new List<INotification>();
            _domainEvents.Add(evt);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }
    }
}