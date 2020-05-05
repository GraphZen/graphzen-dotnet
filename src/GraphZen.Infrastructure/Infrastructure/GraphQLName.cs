// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.Infrastructure
{
    internal static class GraphQLName
    {
        private static readonly Regex NameTokenRegex = new Regex(@"^[_A-Za-z][\w]*$");

        public static bool IsValidGraphQLName(this string name) => NameTokenRegex.IsMatch(name);

        internal const string NameSpecDescription =
            "Names are limited to underscores and alpha-numeric ASCII characters.";

        internal static string GetInvalidNameErrorMessage(string name) =>
            $"Invalid GraphQL Name: '{name}' is not a valid GraphQL name. {NameSpecDescription}.";

        internal static string AssertValidNameArgument(this string name, string paramName)
        {
            try
            {
                name.ThrowIfInvalidGraphQLName();
            }
            catch (InvalidNameException ex)
            {
                throw new ArgumentException(ex.Message, paramName, ex);
            }

            return name;
        }

        internal static void ThrowIfInvalidGraphQLName(this string name)
        {
            if (!name.IsValidGraphQLName())
            {
                throw new InvalidNameException(GetInvalidNameErrorMessage(name));
            }
        }
    }
}