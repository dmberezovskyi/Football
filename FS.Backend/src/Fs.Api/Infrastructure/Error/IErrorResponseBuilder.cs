using System;
using System.Net;

namespace Fs.Api.Infrastructure.Error
{
    public interface IErrorResponseBuilder
    {
        bool Build(Exception exc, out HttpStatusCode code, out ErrorResponseViewModel viewModel);
    }
}