// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen
{
    public static class StringTestExtensions
    {

        public static string Dedent(this string str)
        {
            var trimmedStr =
                str.TrimStart(Environment.NewLine.ToCharArray())
                    .TrimEnd('\t', ' ');

            var indentStr = string.Concat(trimmedStr.TakeWhile(char.IsWhiteSpace));
            var lines = trimmedStr.Split(Environment.NewLine)
                .Select(_ => _.StartsWith(indentStr) ? _.Substring(indentStr.Length) : _);
            var result = lines.ToMultiLineString();
            return result;
        }
    }
}