using FluentValidation;

namespace Fs.Application.Commands.TeamCommands
{
    internal static class RuleBuilderExtensions
    {
        public static IRuleBuilderOptions<T, string> IsTeamName<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.NotEmpty().MaximumLength(64);
        }
    }
}
