using MediatR;

namespace Fs.Application.Queries
{
    public interface IQuery<T> : IRequest<QueryResult<T>>
    {
    }
}
