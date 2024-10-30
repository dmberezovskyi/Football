using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Fs.Application.Commands.AuthCommands;
using Fs.Application.Validation;
using Fs.Domain.Exceptions.Organization;
using Fs.Domain.Exceptions.Team;
using Fs.Domain.Exceptions.User;
using Fs.Infrastructure.Permissions;

namespace Fs.Api.Infrastructure.Error
{
    public class ErrorResponseBuilder : IErrorResponseBuilder
    {
        private readonly Dictionary<Type, Func<Exception, (HttpStatusCode, ErrorViewModel[])>> _builders = new();

        public ErrorResponseBuilder()
        {
            Register();
        }

        public bool Build(Exception exc, out HttpStatusCode code, out ErrorResponseViewModel viewModel)
        {
            if (_builders.TryGetValue(exc.GetType(), out var builder))
            {
                var (httpStatusCode, errorViewModels) = builder(exc);
                code = httpStatusCode;
                viewModel = new ErrorResponseViewModel { Errors = errorViewModels };
                return true;
            }

            code = 0;
            viewModel = null;
            return false;
        }

        private void Register()
        {
            Register<ModelValidationException>(HttpStatusCode.BadRequest, exc => exc.Errors.Select(x => new ErrorViewModel
            {
                Reason = "invalidParameter",
                ErrorCode = x.Code,
                Message = x.Message,
                Location = x.Location,
                Values = x.Values
            }).ToArray());

            Register<InvalidCredentialsException>(HttpStatusCode.BadRequest, "invalidCredentials");
            Register<DuplicateEmailException>(HttpStatusCode.BadRequest, "duplicateEmail");
            Register<ActionDeniedException>(HttpStatusCode.Forbidden, "actionDenied");
            Register<OrganizationNameAlreadyExistsException>(HttpStatusCode.BadRequest, "organizationNameAlreadyExists");
            Register<OrganizationNotFoundException>(HttpStatusCode.BadRequest, "organizationNotFound");
            Register<TeamNameAlreadyExistsException>(HttpStatusCode.BadRequest, "teamNameAlreadyExists");
        }
        private void Register<T>(HttpStatusCode code, Func<T, ErrorViewModel[]> builder)
            where T : Exception
        {
            _builders.Add(typeof(T), exc => (code, builder((T)exc)));
        }
        private void Register<T>(HttpStatusCode code, Func<T, ErrorViewModel> builder)
            where T : Exception
        {
            _builders.Add(typeof(T), exc => (code, new[] { builder((T)exc) }));
        }
        private void Register<T>(HttpStatusCode code, string reason)
            where T : Exception
        {
            _builders.Add(typeof(T), exc => (code, new[]
            {
                new ErrorViewModel { Reason = reason }
            }));
        }
    }
}