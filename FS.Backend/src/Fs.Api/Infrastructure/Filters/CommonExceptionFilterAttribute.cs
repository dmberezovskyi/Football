using System.Net;
using Fs.Api.Infrastructure.Error;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Fs.Api.Infrastructure.Filters
{
    public class CommonExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly IErrorResponseBuilder _errorResponseBuilder;

        public CommonExceptionFilterAttribute(IErrorResponseBuilder errorResponseBuilder)
        {
            _errorResponseBuilder = errorResponseBuilder;
        }

        public override void OnException(ExceptionContext context)
        {
            if (context?.Exception != null && _errorResponseBuilder.Build(context.Exception, out var code, out var viewModel))
            {
                context.Result = new ObjectResult(viewModel)
                {
                    StatusCode = (int) code
                };
                context.Exception = null!;
                context.ExceptionHandled = true;
            }
        }
    }
}
