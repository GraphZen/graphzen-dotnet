// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen
{
    public static class StringAssertionExtensions
    {
        public static AndConstraint<StringAssertions> Be(this StringAssertions assertions, string expected,
            Action<ResultComparisonOptions>? comparisonOptionsAction)
        {
            ResultComparisonOptions? options = null;
            if (comparisonOptionsAction != null)
            {
                options = new ResultComparisonOptions();
                comparisonOptionsAction(options);
            }

            var diff = StringDiffer.GetDiff(expected, assertions.Subject, options);
            var friendlyDiff = diff?.Replace("{", "{{").Replace("}", "}}");

            Execute.Assertion
                .ForCondition(diff == null)
                .FailWith(friendlyDiff);

            return new AndConstraint<StringAssertions>(assertions);
        }

        public static AndConstraint<StringAssertions> Be(this StringAssertions assertions, string expected,
            bool showDiff)
        {
            if (showDiff)
                return assertions.Be(expected, (Action<ResultComparisonOptions>?) null);
            return assertions.Be(expected);
        }
    }
}