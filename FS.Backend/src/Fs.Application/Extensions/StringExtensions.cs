using System;
using System.Linq;

namespace Fs.Application.Extensions
{
    internal static class StringExtensions
    {
        public static string ToCamelCase(this string value)
        {
            var parts = value
                .Split(".", StringSplitOptions.RemoveEmptyEntries)
                .Select(x=> char.ToLowerInvariant(x[0]) + x.Substring(1));
            return string.Join('.', parts);
        }
    }
}
