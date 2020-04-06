using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.LanguageModel.Internal
{
    public static class NameTokenValidator
    {
        public static readonly Regex NameTokenRegex = new Regex(@"^[_A-Za-z][\w]*$");

        public static bool IsValidGraphQLName(this string name) => NameTokenRegex.IsMatch(name);

        public static string ThrowIfInvalidGraphQLName(this string name)
        {
            Check.NotNull(name, nameof(name));
            if (!name.IsValidGraphQLName())
                throw new Exception(
                    $"'{name}' is not a valid GraphQL name. Names are limited to underscores and alpha-numeric ASCII characters.");

            return name;
        }
    }
}