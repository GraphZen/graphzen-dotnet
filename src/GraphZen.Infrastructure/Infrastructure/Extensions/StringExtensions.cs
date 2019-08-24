#nullable disable
// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Linq;
using GraphZen.Infrastructure;

namespace GraphZen.Infrastructure
{
    internal static class StringExtensions
    {
        [NotNull]
        public static string FirstCharToUpper(this string value)
        {
            Check.NotNull(value, nameof(value));

            return value.Length > 1 ? char.ToUpper(value[0]) + value.Substring(1) : value.ToUpper();
        }

        [NotNull]
        public static string FirstCharToLower(this string value)
        {
            Check.NotNull(value, nameof(value));

            return value.Length > 1 ? char.ToLower(value[0]) + value.Substring(1) : value.ToLower();
        }

        internal static string TrimAsyncSuffix(this string value)
        {
            Check.NotNull(value, nameof(value));
            if (value.EndsWith("Async") && value.Length > "Async".Length)
            {
                return value.Substring(0, value.Length - "Async".Length);
            }

            return value;
        }

        private static bool IsUpperCaseAtIndex(this string value, int index) =>
            value != null && value.Length > index && char.IsUpper(value[index]);

        private static bool IsSnakeCase(this string value) => value != null && value.Contains('_');
        private static bool IsKebabCase(this string value) => value != null && value.Contains('-');

        private static bool IsSpaceCase(this string value) => value != null && value.Contains(' ');

        [NotNull]
        public static string ToUpperSnakeCase(this string value)
        {
            Check.NotNull(value, nameof(value));


            if (value.IsSnakeCase())
            {
                return value.ToUpper();
            }

            if (value.IsKebabCase())
            {
                return value.Replace('-', '_').ToUpper();
            }

            if (value.IsSpaceCase())
            {
                return value.Replace(' ', '_').ToUpper();
            }

            var chars = value.SelectMany((c, i) =>
            {
                c = char.ToUpper(c);
                return value.IsUpperCaseAtIndex(i + 1) ? new[] { c, '_' } : new[] { c };
            }).ToArray();

            return new string(chars);
        }
    }
}