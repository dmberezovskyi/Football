using FluentValidation;

namespace Fs.Application.Commands.OrganizationCommands
{
    internal static class RuleBuilderExtensions
    {
        public static IRuleBuilderOptions<T, string> IsOrganizationName<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.NotEmpty().MaximumLength(64);
        }
    }
}
