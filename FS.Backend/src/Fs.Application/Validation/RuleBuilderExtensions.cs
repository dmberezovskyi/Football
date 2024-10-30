using System;
using System.Collections;
using System.Collections.Generic;
using FluentValidation;

namespace Fs.Application.Validation
{
    internal static class RuleBuilderExtensions
    {
        public static IRuleBuilderOptions<T, string> IsEmail<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.NotNull().IsNotEmptyAndWhiteSpace().MaximumLength(64).EmailAddress();
        }
        public static IRuleBuilderOptions<T, string> IsPassword<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.NotNull().IsNotEmptyAndWhiteSpace().MinimumLength(8).MaximumLength(15);
        }
        public static IRuleBuilderOptions<T, string> IsFirstName<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.NotNull().IsNotEmptyAndWhiteSpace().MaximumLength(64);
        }
        public static IRuleBuilderOptions<T, string> IsMiddleName<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.IsNotEmptyAndWhiteSpace().MaximumLength(64);
        }
        public static IRuleBuilderOptions<T, string> IsLastName<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.NotNull().IsNotEmptyAndWhiteSpace().MaximumLength(64);
        }
        public static IRuleBuilderOptions<T, DateTime> IsBirthDate<T>(this IRuleBuilder<T, DateTime> ruleBuilder)
        {
            return ruleBuilder.NotEmpty();
        }
        public static IRuleBuilderOptions<T, string> IsPhone<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.NotEmpty().MaximumLength(16);
        }
        public static IRuleBuilderOptions<T, string> IsUserAbout<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.MaximumLength(256);
        }
        public static IRuleBuilderOptions<T, string> IsNotEmptyAndWhiteSpace<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must((root, value, context) =>
            {
                if (value == null)
                    return true;

                return !string.IsNullOrWhiteSpace(value);
            }).WithErrorCode("IsNotEmptyAndWhiteSpaceValidator");
        }
    }
}
