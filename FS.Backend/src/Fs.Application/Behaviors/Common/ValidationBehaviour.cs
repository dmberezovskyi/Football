using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Fs.Application.Extensions;
using Fs.Application.Validation;
using MediatR;

namespace Fs.Application.Behaviors.Common
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

                if (failures.Count != 0)
                    throw new ModelValidationException(failures.Select(BuildError).ToArray());
            }
            return await next();
        }

        private static Error BuildError(ValidationFailure failure)
        {
            return failure.ErrorCode switch
            {
                "ValuesInArrayValidator" => BuildValuesInArrayValidatorError(failure),
                "ValueInArrayValidator" => BuildValueInArrayValidatorError(failure),
                "InclusiveBetweenValidator" => BuildInclusiveBetweenValidatorError(failure),
                "NotEmptyValidator" => BuildNotEmptyValidatorError(failure),
                "IsNotEmptyAndWhiteSpaceValidator" => BuildIsNotEmptyAndWhiteSpaceValidatorError(failure),
                "NotNullValidator" => BuildNotNullValidatorError(failure),
                "GreaterThanOrEqualValidator" => BuildGreaterThanOrEqualValidatorError(failure),
                "ArrayLengthValidator" => BuildArrayLengthValidatorError(failure),
                "MaximumLengthValidator" => BuildMaximumLengthValidatorError(failure),
                "EmailValidator" => BuildEmailValidatorError(failure),
                "MinimumLengthValidator" => BuildMinimumLengthValidatorError(failure),
                "AsyncPredicateValidator" => BuildAsyncPredicateValidatorError(failure),
                _ => throw new NotSupportedException($"Not supported error: {failure.ErrorCode}")
            };
        }
        private static Error BuildValuesInArrayValidatorError(ValidationFailure failure)
        {
            var values = (dynamic[]) failure.FormattedMessagePlaceholderValues["Values"];
            var invalidValues = (dynamic[]) failure.FormattedMessagePlaceholderValues["InvalidValues"];
            var propName = failure.PropertyName.ToCamelCase();
            var message = $"The '{propName}' has unsupported values: [{string.Join(", ", invalidValues)}]. Supported values are: [{string.Join(", ", values)}]";

            return new Error("unsupportedValues", message, propName);
        }
        private static Error BuildValueInArrayValidatorError(ValidationFailure failure)
        {
            var values = (dynamic[])failure.FormattedMessagePlaceholderValues["Values"];
            var propName = failure.PropertyName.ToCamelCase();
            var message = $"The '{propName}' value is unsupported. Supported values are: [{string.Join(", ", values)}]";

            return new Error("unsupportedValue", message, propName);
        }
        private static Error BuildInclusiveBetweenValidatorError(ValidationFailure failure)
        {
            var from = failure.FormattedMessagePlaceholderValues["From"];
            var to = failure.FormattedMessagePlaceholderValues["To"];
            var propName = failure.PropertyName.ToCamelCase();
            var message = $"The '{propName}' value must be between {from} and {to}.";

            return new Error("inclusiveBetween", message, failure.PropertyName.ToCamelCase(), new Dictionary<string, object>
            {
                { "from", from },
                { "to", to }
            });
        }
        private static Error BuildIsNotEmptyAndWhiteSpaceValidatorError(ValidationFailure failure)
        {
            var propName = failure.PropertyName.ToCamelCase();
            return new Error("empty", $"The '{propName}' cannot be empty or contain only white space.", propName);
        }
        private static Error BuildNotEmptyValidatorError(ValidationFailure failure)
        {
            var propName = failure.PropertyName.ToCamelCase();
            return new Error("empty", $"The '{propName}' cannot be empty.", propName);
        }
        private static Error BuildNotNullValidatorError(ValidationFailure failure)
        {
            var propName = failure.PropertyName.ToCamelCase();
            return new Error("required", $"The '{propName}' value is required.", propName);
        }
        private static Error BuildGreaterThanOrEqualValidatorError(ValidationFailure failure)
        {
            var propName = failure.PropertyName.ToCamelCase();
            var comparisonValue = failure.FormattedMessagePlaceholderValues["ComparisonValue"];
            return new Error("greaterThanOrEqual", $"The '{propName}' value must be greater than or equal to {comparisonValue}.", propName, new Dictionary<string, object>
            {
                { "comparisonValue", comparisonValue }
            });
        }
        private static Error BuildArrayLengthValidatorError(ValidationFailure failure)
        {
            var propName = failure.PropertyName.ToCamelCase();
            var comparisonValue = failure.FormattedMessagePlaceholderValues["ComparisonValue"];
            return new Error("itemsCountLimit", $"The '{propName}' items count must be less than or equal to {comparisonValue}", propName, new Dictionary<string, object>
            {
                { "comparisonValue", comparisonValue }
            });
        }
        private static Error BuildMaximumLengthValidatorError(ValidationFailure failure)
        {
            var propName = failure.PropertyName.ToCamelCase();
            var maxLength = failure.FormattedMessagePlaceholderValues["MaxLength"];
            return new Error("maxLength", $"The '{propName}' value is too long. The maximum value length is {maxLength}", propName, new Dictionary<string, object>
            {
                { "maxLength", maxLength }
            });
        }
        private static Error BuildEmailValidatorError(ValidationFailure failure)
        {
            var propName = failure.PropertyName.ToCamelCase();
            return new Error("invalidEmailFormat", $"The '{propName}' value has invalid email format.", propName);
        }
        private static Error BuildMinimumLengthValidatorError(ValidationFailure failure)
        {
            var propName = failure.PropertyName.ToCamelCase();
            var minLength = failure.FormattedMessagePlaceholderValues["MinLength"];
            return new Error("minLength", $"The '{propName}' value is too short. The minimum value length is {minLength}", propName, new Dictionary<string, object>
            {
                { "minLength", minLength }
            });
        }
        private static Error BuildAsyncPredicateValidatorError(ValidationFailure failure)
        {
            var propName = failure.PropertyName.ToCamelCase();
            return new Error(null, failure.ErrorMessage, propName);
        }
    }
}
