// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.Infrastructure
{
    internal static class GraphQLName
    {
        internal const string NameSpecDescription =
            "Names are limited to underscores and alpha-numeric ASCII characters.";

        private static readonly Regex NameTokenRegex = new Regex(@"^[_A-Za-z][\w]*$");

        public static bool IsValidGraphQLName(this string name) => NameTokenRegex.IsMatch(name);
    }
}