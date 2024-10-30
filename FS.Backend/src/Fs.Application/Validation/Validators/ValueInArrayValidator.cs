using System.Linq;
using FluentValidation;
using FluentValidation.Validators;

namespace Fs.Application.Validation.Validators
{
    internal sealed class ValueInArrayValidator<T, TProperty> : PropertyValidator<T, TProperty>
    {
        public override string Name => "ValueInArrayValidator";

        private readonly TProperty[] _values;

        public ValueInArrayValidator(TProperty[] values)
        {
            _values = values;
        }

        public override bool IsValid(ValidationContext<T> context, TProperty value)
        {
            if (value == null)
                return true;

            if (_values.Contains(value))
                return true;

            context.MessageFormatter
                .AppendArgument("Values", _values);

            return false;
        }
    }
}
