using System;
using MediatR;

namespace Fs.Domain.SeedWork
{
    public interface IDomainEvent : INotification
    {
        Guid Id { get; set; }
    }
}
