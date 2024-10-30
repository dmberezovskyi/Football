using System.Threading;
using System.Threading.Tasks;
using Fs.Domain.Services;
using Fs.Infrastructure.Storage.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Fs.Application.Services
{
    internal sealed class UserService : IUserService
    {
        private readonly IRepository _repository;

        public UserService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> HasEmailAsync(string email, CancellationToken cancellationToken)
        {
            email = email.Trim().ToLowerInvariant();
            return await _repository.Get<Domain.Aggregates.UserAggregate.User>(x => x.Email == email).AnyAsync(cancellationToken);
        }
    }
}
