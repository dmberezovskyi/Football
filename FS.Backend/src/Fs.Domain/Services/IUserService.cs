using System.Threading;
using System.Threading.Tasks;

namespace Fs.Domain.Services
{
    public interface IUserService
    {
        Task<bool> HasEmailAsync(string email, CancellationToken cancellationToken);
    }
}
