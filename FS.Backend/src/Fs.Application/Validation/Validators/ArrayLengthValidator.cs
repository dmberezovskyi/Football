using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using FluentValidation.Validators;

namespace Fs.Application.Validation.Validators
{
    internal sealed class ArrayLengthValidator<T, TProperty> : PropertyValidator<T, IEnumerable<TProperty>>
    {
        public override string Name => "ArrayLengthValidator";

        private readonly int _valueToCompare;

        public ArrayLengthValidator(int valueToCompare)
        {
            _valueToCompare = valueToCompare;
        }

        public override bool IsValid(ValidationContext<T> context, IEnumerable<TProperty> value)
        {
            if (value == null)
                return true;

            if (value.Count() <= _valueToCompare) 
                return true;

            context.MessageFormatter
                .AppendArgument("ComparisonValue", _valueToCompare);

            return false;

        }
    }
}
