using System;
using System.Threading.Tasks;

namespace Fs.Domain.Services
{
    public interface IHtmlRenderService
    {
        Task<string> RenderAsync(string templateName, object model = null, string cultureName = null);
    }
}
