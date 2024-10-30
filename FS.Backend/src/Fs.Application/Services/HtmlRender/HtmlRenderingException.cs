using System;

namespace Fs.Application.Services.HtmlRender
{
    public class HtmlRenderingException : Exception
    {
        public HtmlRenderingException()
        {
        }
        public HtmlRenderingException(string message)
            : base(message)
        {
        }
        public HtmlRenderingException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
