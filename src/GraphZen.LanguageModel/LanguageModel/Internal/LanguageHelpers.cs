// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
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


    public static class LanguageHelpers
    {
        private static readonly Regex NewlineRegex = new Regex("\r\n?|\n");

        internal static bool HasNewline(this string value) => NewlineRegex.IsMatch(value);


        public static string BlockStringValue(string rawString)
        {
            var lines = rawString.Split(new[] {Environment.NewLine}, int.MaxValue, StringSplitOptions.None).ToList();
            int? commonIndent = null;
            for (var index = 1; index < lines.Count; index++)
            {
                var line = lines[index];
                Debug.Assert(line != null, nameof(line) + " != null");
                var indent = line.TakeWhile(char.IsWhiteSpace).Count();
                if (indent < line.Length && (commonIndent == null || indent < commonIndent.Value))
                {
                    commonIndent = indent;
                    if (commonIndent == 0) break;
                }
            }


            if (commonIndent.HasValue)
                for (var i = 1; i < lines.Count; i++)
                {
                    var line = lines[i];
                    Debug.Assert(line != null, nameof(line) + " != null");
                    if (line.Length > commonIndent.Value) lines[i] = line.Substring(commonIndent.Value);
                }

            while (lines.Count > 0 && string.IsNullOrWhiteSpace(lines[0]))
            {
                lines.RemoveAt(0);
            }

            while (lines.Count > 0 && string.IsNullOrWhiteSpace(lines[lines.Count - 1]))
            {
                lines.RemoveAt(lines.Count - 1);
            }


            return string.Join(Environment.NewLine, lines);
        }

        public static string BlockStringValueOld(string rawString)
        {
            Check.NotNull(rawString, nameof(rawString));
            var lines = NewlineRegex.Split(rawString).ToList();

            int? commonIndent = null;
            foreach (var line in lines)
            {
                var indent = line.GetIndent();
                if (indent < line.Length && (commonIndent == null || indent < commonIndent))
                {
                    commonIndent = indent;
                    if (commonIndent == 0) break;
                }
            }

            if (commonIndent.HasValue)
                for (var i = 1; i < lines.Count; i++)
                {
                    var line = lines[i];
                    Debug.Assert(line != null, nameof(line) + " != null");
                    if (line.Length > commonIndent.Value) lines[i] = line.Substring(commonIndent.Value);
                }

            while (lines.Count > 0 && string.IsNullOrWhiteSpace(lines[0]))
            {
                lines.RemoveAt(0);
            }

            while (lines.Count > 0 && string.IsNullOrWhiteSpace(lines[lines.Count - 1]))
            {
                lines.RemoveAt(lines.Count - 1);
            }


            return string.Join(Environment.NewLine, lines);
        }


        private static int GetIndent(this string str)
        {
            var i = 0;
            while (i < str.Length && (str[i] == ' ' || str[i] == '\t'))
            {
                i++;
            }

            return i;
        }
    }
}