using System.Collections.Generic;
using FluentValidation;
using Fs.Application.Models;
using Fs.Application.Validation.Validators;

namespace Fs.Application.Validation
{
    public static class ValidatorExtensions
    {
        public static IRuleBuilderOptions<T, TProperty[]> IsInArray<T, TProperty>(this IRuleBuilder<T, TProperty[]> ruleBuilder, params TProperty[] values)
        {
            return ruleBuilder.SetValidator(new ValuesInArrayValidator<T, TProperty>(values));
        }
        public static IRuleBuilderOptions<T, TProperty> IsInArray<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, params TProperty[] values)
        {
            return ruleBuilder.SetValidator(new ValueInArrayValidator<T, TProperty>(values));
        }
        public static IRuleBuilderOptions<T, IEnumerable<TProperty>> IsLengthLessThanOrEqualTo<T, TProperty>(this IRuleBuilder<T, IEnumerable<TProperty>> ruleBuilder, int valueToCompare)
        {
            return ruleBuilder.SetValidator(new ArrayLengthValidator<T, TProperty>(valueToCompare));
        }
        public static IRuleBuilderOptions<T, Address> IsAddress<T>(this IRuleBuilder<T, Address> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new AddressValidator());
        }
    }
}
