// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

#nullable enable


namespace GraphZen.Infrastructure
{
    public static class StringUtils
    {
        private const int OrListMaxLength = 5;

        public static string QuotedOrList(this IReadOnlyList<string> source)
        {
            // TODO: source = Check.NotEmpty(source, nameof(source));
            if (source.Count == 0) throw new ArgumentException("Must contain at least one value.", nameof(source));
            return OrList(source.Select(v => $"\"{v}\"").ToArray());
        }

        public static string QuotedOrList(params string[] values) => values.QuotedOrList();

        public static string OrList(
            this IEnumerable<string> items)
        {
            var selected = items.Take(OrListMaxLength).ToArray();
            return selected.Select((quoted, index) => (quoted, index)).Aggregate("",
                (list, value) =>
                {
                    var (quoted, index) = value;
                    if (index == 0) return quoted;

                    return list + (selected.Length > 2 ? ", " : " ") +
                           (index == selected.Length - 1 ? "or " : "") + quoted;
                });
        }

        public static IEnumerable<string> GetSuggestionList(string input, params string[] options) =>
            GetSuggestionList(Check.NotNull(input, nameof(input)),
                (IEnumerable<string>) Check.NotNull(options, nameof(options)));


        public static IReadOnlyList<string> GetSuggestionList(string input,
            IEnumerable<string> options)
        {
            var optionsByDistance = new Dictionary<string, int>();
            var inputThreshold = input.Length / 2;
            foreach (var option in options)
            {
                var distance = GetLexicalDistance(input, option);
                var threshold = Math.Max(Math.Max(inputThreshold, option.Length / 2), 1);
                if (distance <= threshold) optionsByDistance[option] = distance;
            }

            return optionsByDistance.OrderBy(_ => _.Value).Select(_ => _.Key).ToArray();
        }

        public static int GetLexicalDistance(string a, string b)
        {
            Check.NotNull(a, nameof(a));
            Check.NotNull(b, nameof(b));
            a = a.ToLower();
            b = b.ToLower();
            var aLength = a.Length;
            var bLength = b.Length;
            var d = new int[aLength + 1, bLength + 1];

            if (aLength == 0) return bLength;

            if (bLength == 0) return aLength;

            for (var i = 0; i <= aLength; d[i, 0] = i++)
            {
            }

            for (var j = 0; j <= bLength; d[0, j] = j++)
            {
            }

            for (var i = 1; i <= aLength; i++)
            for (var j = 1; j <= bLength; j++)
            {
                var cost = b[j - 1] == a[i - 1] ? 0 : 1;

                d[i, j] = Math.Min(
                    Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                    d[i - 1, j - 1] + cost);
            }

            return d[aLength, bLength];
        }
    }
}