using System;
using MediatR;

namespace Fs.Application.Commands.Base
{
    public interface ICommand<out T> : IRequest<T>
    {
        Guid Id { get; set; }
        int Version { get; set; }
    }
    public abstract class BaseCommand<T> : ICommand<T>
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
    }
    public abstract class BaseCommand : BaseCommand<Unit>
    { 
    }
}
