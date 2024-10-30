using System.Threading.Tasks;

namespace Fs.Domain.Services
{
    public interface IEmailService
    {
        Task SendAsync(string recipient, string subject, string htmlBody);
    }
}
