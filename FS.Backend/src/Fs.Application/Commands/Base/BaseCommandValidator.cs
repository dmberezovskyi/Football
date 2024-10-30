using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace Fs.Application.Commands.Base
{
    public abstract class BaseCommandValidator<TCommand, TResult> : AbstractValidator<TCommand>
        where TCommand : ICommand<TResult>
    {
        protected BaseCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }

    public abstract class BaseCommandValidator<TCommand> : BaseCommandValidator<TCommand, Unit>
        where TCommand : ICommand<Unit>
    {
    }
}
