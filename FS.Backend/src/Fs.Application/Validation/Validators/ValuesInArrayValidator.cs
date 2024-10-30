using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using FluentValidation.Validators;

namespace Fs.Application.Validation.Validators
{
    internal sealed class ValuesInArrayValidator<T, TProperty> : PropertyValidator<T, TProperty[]>
    {
        public override string Name => "ValuesInArrayValidator";

        private readonly TProperty[] _values;

        public ValuesInArrayValidator(TProperty[] values)
        {
            _values = values;
        }

        public override bool IsValid(ValidationContext<T> context, TProperty[] value)
        {
            if (value == null)
                return true;

            var invalidValues = value.Except(_values).ToArray();

            if (!invalidValues.Any())
                return true;

            context.MessageFormatter
                .AppendArgument("Values", _values)
                .AppendArgument("InvalidValues", invalidValues);

            return false;

        }
    }
}
