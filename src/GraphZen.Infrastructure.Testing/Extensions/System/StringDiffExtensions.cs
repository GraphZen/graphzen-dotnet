// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using DiffPlex;
using DiffPlex.DiffBuilder;
using DiffPlex.DiffBuilder.Model;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.Infrastructure
{
    public static class StringDiffExtensions
    {
        public static string? GetDiff(this string actual, string expected) =>
            GetDiff(actual, expected, (StringDiffOptions?) null);

        public static string? GetDiff(this string actual, string expected,
            Action<StringDiffOptions>? comparisonOptionsAction)
        {
            var options = StringDiffOptions.FromOptionsAction(comparisonOptionsAction);
            return actual.GetDiff(expected, options);
        }

        public static string? GetDiff(this string actual, string expected, StringDiffOptions? options) =>
            TryGetDiff(actual, expected, out var differences, options) ? differences : null;

        private static bool TryGetDiff(string actual, string expected, out string differences,
            StringDiffOptions? options = null)
        {
            options = options ?? new StringDiffOptions();
            var diffStringBuilder = new StringBuilder();
            var diffBuilder = new InlineDiffBuilder(new Differ());
            var diff = diffBuilder.BuildDiffModel(actual, expected);

            foreach (var line in diff.Lines)
            {
                switch (line.Type)
                {
                    case ChangeType.Inserted:
                        diffStringBuilder.Append("+ ");
                        break;
                    case ChangeType.Deleted:
                        diffStringBuilder.Append("- ");
                        break;
                    default:
                        diffStringBuilder.Append(" ");
                        break;
                }

                diffStringBuilder.AppendLine(line.Text);
            }

            var hasDiff = diff.Lines.Any(_ => _.Type != ChangeType.Unchanged);

            var diffString = diffStringBuilder.ToString();
            if (!hasDiff)
            {
                var expectedDetail = Json.SerializeObject(expected);
                var actualDetail = Json.SerializeObject(actual);
                hasDiff = expectedDetail != actualDetail;
                diffString = $@"
                Expected: {expectedDetail}
                Actual:   {actualDetail}
                ".Dedent();
            }


            var errorMessage = hasDiff ? "Differences found " : "No differences found";
            errorMessage += "\n";
            if (options.ShowActual) errorMessage += $"=== Actual ===\n{actual}\n";

            if (options.ShowExpected) errorMessage += $"=== Expected ===\n{expected}\n";

            if (options.ShowDiffs) errorMessage += $"=== Differences ===\n{diffString}\n";

            differences = errorMessage;
            return hasDiff;
        }
    }
}